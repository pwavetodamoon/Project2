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
        Level = heroData.Level;
        maxHealth = heroData.maxHealth;
        health.Value = heroData.health.Value;
        BaseDamage.Value = heroData.BaseDamage.Value;
        speed.Value = heroData.speed.Value;
        CritRate.Value = heroData.CritRate.Value;
        CritDamage.Value = heroData.CritDamage.Value;
        attackCoolDown.Value = heroData.attackCoolDown.Value;
    }
}
