using CombatSystem.Entity;
using CombatSystem.HeroDataManager.Data;
using UnityEngine;

namespace LevelAndStats
{
    public class HeroEntityStats : EntityStats
    {
        // nang cap hero
        [SerializeField] private HeroData heroData;

        public void SetHero(HeroData newData)
        {
            heroData = newData;
            LoadEntityStats();
        }

        private void LoadEntityStats()
        {
            structStats = heroData.structStats;
        }

        public void Upgrade()
        {
            structStats.level += 1;
            structStats.baseDamage += 1.0f;
        }
    }
}