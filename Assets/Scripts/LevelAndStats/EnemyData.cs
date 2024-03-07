using UnityEngine;

namespace LevelAndStats
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public StructStats structStats;
    }
}