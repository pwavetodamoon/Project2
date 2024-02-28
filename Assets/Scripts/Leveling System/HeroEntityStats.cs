using Leveling_System;
using NewCombat.HeroDataManager.Data;

public class HeroEntityStats : EntityStats
{
    public HeroData heroData;

    private void Awake()
    {
        LoadData();
    }
    public void LoadData()
    {
        structStats = heroData.structStats;
    }
}
