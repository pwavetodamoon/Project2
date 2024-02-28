using System.Collections;
using System.Collections.Generic;
using Leveling_System;
using NewCombat.HeroDataManager.Data;
using UnityEngine;

public class EnemyStats : EntityStats
{
    public EnemyData EnemyData;
    public void LoadData()
    {
        Level = EnemyData.Level;
        maxHealth = EnemyData.maxHealth;
        health.Value = EnemyData.health.Value;
        BaseDamage.Value = EnemyData.BaseDamage.Value;
        speed.Value = EnemyData.speed.Value;
        CritRate.Value = EnemyData.CritRate.Value;
        CritDamage.Value = EnemyData.CritDamage.Value;
        attackCoolDown.Value = EnemyData.attackCoolDown.Value;
    }
}
