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

    private void OnValidate()
    {
        if (stageInformation == null)
        {
            stageInformation = GetDataSupport.Get().StageInformation;
        }
    }

    public void SetStatsByLevel(EnemyStats stats)
    {
        var level = stageInformation.GetLevelForMonster();
        stats.SetStructStats(EnemyData.GetEnemyStats(level));
    }
}