using Leveling_System;
using NewCombat.HeroDataManager.Data;
using UnityEngine;

public class HeroEntityStats : EntityStats
{
    [SerializeField] private HeroData heroData;

    public void SetHero(HeroData newData)
    {
        heroData = newData;
        LoadData();
    }
    public void LoadData()
    {
        structStats = heroData.structStats;
    }
}
