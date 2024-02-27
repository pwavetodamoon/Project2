using Characters;
using Leveling_System;
using NewCombat.AttackFactory;
using NewCombat.Characters;
using UnityEngine;

namespace NewCombat.HeroDataManager.Data
{
    [CreateAssetMenu(fileName = "Hero", menuName = "CombatSystem/Hero", order = 1)]
    public class BaseData : ScriptableObject
    {
        // TODO: Split monster data in game and monster data in scriptable object
        // NOTE: Monster data in game will be changed in game, monster data in scriptable object will be changed in editor
        // EnemyData just use like a template for monster data in game, and monster stats will different in each monster

        // TODO: Make a properties Level for player and monster (maybe).
        // With player, stats will increase by player level
        [SerializeField] protected string heroName;

        [SerializeField] protected AttackTypeEnum AttackType;
        [SerializeField] protected CharacterEnumType characterEnumType;
        [SerializeField] protected BaseStat baseStats;
        [SerializeField] protected int slotIndex = 0;
        [SerializeField] private HeroSingleAttackFactory HeroSingleAttackFactory;
        private GameObject heroObject;

        public int SlotIndex
        {
            get => slotIndex;
            set => slotIndex = value;
        }

        public void GetDataForHero(GameObject heroObject)
        {
            heroObject.name = heroName;
            var hero = heroObject.GetComponent<HeroCharacter>();
            hero.SetAttackFactory(HeroSingleAttackFactory);
            hero.InGameSlotIndex = slotIndex;
            this.heroObject = heroObject;
        }
    }

    public enum AttackTypeEnum
    {
        Near,
        Far
    }
}