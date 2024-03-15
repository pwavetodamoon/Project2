using System.Collections;
using System.Collections.Generic;
using LevelAndStats;
using UnityEngine;

public class MonstersStatsSystem : MonoBehaviour
{
    public EnemyData EnemyData;
    public StageInformation stageInformation;

    /// <summary>
    /// Chỉnh stats bằng hàm từ Enemy Data tương ứng với level trung bình của màn chơi
    /// </summary>
    /// <param name="stats"></param>
    public void SetStatsByLevel(EnemyStats stats)
    {
        var level = stageInformation.GetLevelForMonster();
        stats.SetStructStats(EnemyData.GetEnemyStats(level));
    }
}
