using Leveling_System;

public class EnemyStats : EntityStats
{
    public EnemyData EnemyData;

    public void LoadData()
    {
        structStats = EnemyData.structStats;
    }
}