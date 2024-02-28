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
        structStats = EnemyData.structStats;
    }
}
