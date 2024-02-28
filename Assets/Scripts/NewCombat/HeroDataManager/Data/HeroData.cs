using Characters;
using Leveling_System;
using NewCombat.AttackFactory;
using NewCombat.Characters;
using UnityEngine;

namespace NewCombat.HeroDataManager.Data
{
    [CreateAssetMenu(fileName = "Hero", menuName = "CombatSystem/Hero", order = 1)]

    public class HeroData : ScriptableObject
    {
        public StructStats structStats;
        public Sprite icon;
        [SerializeField] protected string heroName;
        [SerializeField] protected AttackTypeEnum AttackType;
        [SerializeField] protected CharacterEnumType characterEnumType;
        [SerializeField] protected BaseStat baseStats;
        [SerializeField] protected int slotIndex = 0;
        [SerializeField] private HeroSingleAttackFactory HeroSingleAttackFactory;
        public HeroCharacter heroCharacter;

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

        public enum AttackTypeEnum
        {
            Near,
            Far
        }
    }
}