using Leveling_System;
using NewCombat.HeroDataManager.Data;
using UnityEngine;

public class HeroEntityStats : EntityStats
{
    [SerializeField] private HeroData heroData;

    public void SetHero(HeroData newData)
    {
        heroData = newData;
        LoadEntityStats();
    }
    public void LoadEntityStats()
    {
        structStats = heroData.structStats;
    }
}
