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
    }
}