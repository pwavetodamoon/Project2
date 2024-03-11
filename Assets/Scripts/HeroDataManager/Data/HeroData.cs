using System.Collections.Generic;
using CombatSystem.Attack.Factory;
using CombatSystem.Entity;
using LevelAndStats;
using Model.Hero;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;

namespace CombatSystem.HeroDataManager.Data
{
    [CreateAssetMenu(fileName = "Hero", menuName = "CombatSystem/Hero", order = 1)]
    public class HeroData : SerializedScriptableObject
    {
        public enum AttackTypeEnum
        {
            Near,
            Far
        }

        [SerializeField] protected AttackTypeEnum AttackType;
        [SerializeField] protected CharacterEnumType characterEnumType;
        [SerializeField] private HeroSingleAttackFactory HeroSingleAttackFactory;
        [SerializeField] public int slotIndex;
        [SerializeField] public string heroName;

        public bool isDead;
        public HeroCharacter heroCharacter;
        public StructStats structStats;
        public Sprite icon;
        [FolderPath(ParentFolder = "Assets/Resources")]
        public string resourcePath;

        [ShowInInspector] private Dictionary<Character_Body_Sprites.SpritePartEnum, Sprite> spriteDictionary;

        public HeroSingleAttackFactory GetHeroFactory()
        {
            return HeroSingleAttackFactory;
        }

        public void OnSaveSlotIndex()
        {
            slotIndex = heroCharacter.InGameSlotIndex;
        }

        public Dictionary<Character_Body_Sprites.SpritePartEnum, Sprite> GetSkinDictionary()
        {
            if (spriteDictionary == null) LoadAllSkinInFolder();
            return spriteDictionary;
        }

        [Button]
        public void LoadFromHeroInGame()
        {
            // TODO: Add is dead field
            if (heroCharacter == null) return;
            structStats = heroCharacter.GetComponent<HeroEntityStats>().GetStructStats();
            slotIndex = heroCharacter.InGameSlotIndex;
            isDead = heroCharacter.IsDead;
        }

        public void LoadFromHeroSaveGame(HeroCloudSaveData heroCloudSave)
        {
            heroName = heroCloudSave.heroName;
            structStats = heroCloudSave.structStats;
            slotIndex = heroCloudSave.slotIndex;
            isDead = heroCloudSave.isDead;
        }


        [Button]
        private void LoadAllSkinInFolder()
        {
            var sprites = Resources.LoadAll<Sprite>(resourcePath);
            LoadSpriteHelp.LoadSpritePart(sprites, out spriteDictionary);
        }
    }
}