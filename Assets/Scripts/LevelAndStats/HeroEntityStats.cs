using NewCombat.HeroDataManager.Data;
using UnityEngine;

namespace LevelAndStats
{
    public class HeroEntityStats : EntityStats
    {
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