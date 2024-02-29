﻿using NewCombat.Characters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using CombatSystem;
using Helper;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewCombat.Slots
{
    public enum SlotState
    {
        Empty,
        Occupied
    }
    public class SlotManager : Singleton<SlotManager>
    {
        public List<HeroSlotInGame> Slots = new();
        [FormerlySerializedAs("BannedSlot")] public BannedSlotControl bannedSlotControl;
        private void Start()
        {
            // Load các slot vào vị trí giữa grid
            var combatGrid = GridManager.Instance.GetGrid();
            foreach (var slot in Slots)
            {
                if(slot.SlotIndex == -1) continue;
                var position = slot.transform.position;
                combatGrid.GetXY(position, out var x, out var y);
                combatGrid.SetValue(position, 1);

                slot.transform.localPosition = combatGrid.GetCenterGridWorldPosition(x, y);
                slot.enemyStand.position = combatGrid.GetCenterGridWorldPosition(x + 1, y);

            }

            
        }

        public void LoadHeroIntoSlotInGame(List<HeroCharacter> heroList)
        {
            //var heroList = new List<GameObject>(CombatEntitiesManager.Instance.GetHeroList());
            if (heroList.Count == 0)
            {
                Debug.LogError("Hero list is empty");
                return;
            }

            foreach (var hero in heroList)
            {
                //var heroCharacter = hero.GetComponent<HeroCharacter>();
                // -1 nghĩa là ko có trong game
                if (hero.InGameSlotIndex == -1)
                {
                    bannedSlotControl.SetHeroIntoStandPosition(hero.transform);
                    hero.SetSlotIndex(bannedSlotControl.SlotIndex);
                    continue;
                }
                // Nếu slot của hero đó đã có hero khác thì ko cho vào
                var slot = GetSlotBySlotIndexInRange(hero.InGameSlotIndex);
                if (slot.currentHero == null)
                {
                    slot.SetHeroIntoStandPosition(hero.transform);
                }
                else
                {
                }
            }
        }
        public bool FindNearSlotAndSwapIfInRange(HeroCharacter hero, int heroIndex)
        {
            if (Slots.Count == 0) return false;
            Vector3 position = hero.transform.position;

            float minSqrMagnitude = (Slots[0].transform.position - position).sqrMagnitude;
            var minSlot = Slots[0];
            foreach (var slot in Slots)
            {
                var newSqrMagnitude = (slot.transform.position - position).sqrMagnitude;

                if (minSqrMagnitude == 0 || newSqrMagnitude < minSqrMagnitude)
                {
                    minSqrMagnitude = newSqrMagnitude;
                    minSlot = slot;
                }
            }
            bool isInRange = minSqrMagnitude < minSlot.radius * minSlot.radius;

            //Debug.Log("Min Slot Name: "+minSlot.name);
            //Debug.Log("Min Slot Index: "+minSlot.SlotIndex);
            //Debug.Log("Is in range of slot: " + isInRange);
            if (isInRange)
            {
                SwapOneWay(heroIndex, minSlot.SlotIndex);
            }
            return isInRange;
        }

        [Button]
        public void SwapOneWay(int currentSlotIndex, int targetSlotIndex)
        {
            // TODO: it not work if current slot index is -1, add logic for this case
            if (currentSlotIndex == -1 && targetSlotIndex != -1)
            {
                SwapFromBannedSlotToInGameSlot(targetSlotIndex);
                return;
            }

            if (currentSlotIndex != -1 && targetSlotIndex == -1)
            {
                SwapSlotIntoBannedSlot(currentSlotIndex);
                return;
            }

            // TODO: Add logic to add hero to banned slot
            if (currentSlotIndex < 0 || targetSlotIndex < 0 || currentSlotIndex > Slots.Count || targetSlotIndex > Slots.Count)
            {
                return;
            }
            var currentSlot = GetSlotBySlotIndexInRange(currentSlotIndex);
            var targetSlot = GetSlotBySlotIndexInRange(targetSlotIndex);

            var currentHero = currentSlot.currentHero;
            var targetHero = targetSlot.currentHero;

            currentSlot.SetHeroIntoStandPosition(targetHero);
            targetSlot.SetHeroIntoStandPosition(currentHero);

            if(currentHero != null)
                currentHero.GetComponent<HeroCharacter>().SetSlotIndex(targetSlotIndex);
            if(targetHero != null)
                targetHero.GetComponent<HeroCharacter>().SetSlotIndex(currentSlotIndex);

        }

        private void SwapSlotIntoBannedSlot(int currentSlotIndex)
        {
            var currentSlot = GetSlotBySlotIndexInRange(currentSlotIndex);
            var currentHero = currentSlot.currentHero;
            bannedSlotControl.SetHeroIntoStandPosition(currentHero.transform);
            currentHero.GetComponent<HeroCharacter>().SetSlotIndex(bannedSlotControl.SlotIndex);
            currentSlot.SetHeroIntoStandPosition(null);
        }
        private void SwapFromBannedSlotToInGameSlot(int targetSlotIndex)
        {
            var targetSlot = GetSlotBySlotIndexInRange(targetSlotIndex);
            var targetHero = targetSlot.currentHero;

            if (targetHero != null)
            {
                bannedSlotControl.SetHeroIntoStandPosition(targetHero.transform);
                targetHero.GetComponent<HeroCharacter>().SetSlotIndex(bannedSlotControl.SlotIndex);
            }
            var bannedHero = SelectionHero.Instance.heroOfUI;

            targetSlot.SetHeroIntoStandPosition(bannedHero);
            bannedHero.GetComponent<HeroCharacter>().SetSlotIndex(targetSlotIndex);

            SelectionHero.Instance.heroOfUI = null;

        }
        public void SetHeroBackToOriginalSlot(HeroCharacter hero, int heroIndex)
        {
            if (heroIndex == -1)
            {
                bannedSlotControl.SetHeroIntoStandPosition(hero.transform);
                return;
            }

            var slot = GetSlotBySlotIndexInRange(heroIndex);
            hero.transform.position = slot.GetCharacterPosition().position;
        }


        private HeroSlotInGame GetSlotBySlotIndexInRange(int index)
        {
            return Slots[index];
        }



        public Transform GetStandTransform(int index)
        {
            var slot = GetSlotBySlotIndexInRange(index);
            return slot.GetCharacterPosition();
        }

        public Transform GetAttackerTransform(int index)
        {
            var slot = GetSlotBySlotIndexInRange(index);
            return slot.GetAttackerPosition();
        }

        public bool TryGetSlotNearPosition(Vector2 position, out HeroSlotInGame nearSlotInGame)
        {
            nearSlotInGame = null;
            float minSqrMagnitude = 0;
            foreach (var slot in Slots)
            {
                var newSqrMagnitude = (slot.transform.position - (Vector3)position).sqrMagnitude;
                if (minSqrMagnitude == 0 || newSqrMagnitude < minSqrMagnitude)
                {
                    minSqrMagnitude = newSqrMagnitude;
                    nearSlotInGame = slot;
                }
            }

            if (nearSlotInGame == null ||
                minSqrMagnitude > 15 ||
                minSqrMagnitude > (nearSlotInGame.radius * nearSlotInGame.radius))
            {
                return false;
            }
            //Debug.Log("Min Sqr Magnitude: " + minSqrMagnitude);
            //var direction = nearSlotInGame.transform.position - (Vector3)position;
            //Debug.DrawRay(position, direction, Color.red);
            //Debug.Log("Near Slot Name: " + nearSlotInGame.name);
            return nearSlotInGame;
        }
    }
}