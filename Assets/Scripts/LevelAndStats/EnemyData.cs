using CombatSystem.Entity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelAndStats
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private MonsterCharacter monsterPrefab;
        [SerializeField] private StructStats structStats;
        [SerializeField] private ScaleValue healthScaleValue;
        [SerializeField] private ScaleValue damageScaleValue;


        public MonsterCharacter MonsterPrefab
        {
            get
            {
                if (monsterPrefab == null)
                {
                    Debug.LogError("MonsterPrefab is null");
                }
                return monsterPrefab;
            }
        }
        public StructStats GetEnemyStats(int level)
        {
            var stats = structStats;
            stats.level = level;
            stats.health = GetRandomHealthByLevel(level);
            stats.baseDamage = GetRandomDamageByLevel(level);
            return stats;
        }

        [Button]
        public void CreateData()
        {
            healthScaleValue = new ScaleValue(structStats.maxHealth, 1.1f, 1f, 1.3f);
            damageScaleValue = new ScaleValue(structStats.baseDamage, 1.2f, 1f, 1.5f);
        }

        private float GetRandomHealthByLevel(int level)
        {
            return healthScaleValue.GetRandomValue(level);
        }

        private float GetRandomDamageByLevel(int level)
        {
            return damageScaleValue.GetRandomValue(level);
        }
    }

    [System.Serializable]
    public class ScaleValue
    {
        public float baseValue = 1;
        public float currentScale = 1.0f;
        [Header("Random")]
        public float minRangeValue = .1f;
        public float maxRangeValue = .2f;

        public ScaleValue(float baseValue, float currentScale, float minRangeValue, float maxRangeValue)
        {
            this.baseValue = baseValue;
            this.currentScale = currentScale;
            this.minRangeValue = minRangeValue;
            this.maxRangeValue = maxRangeValue;
        }
        [Button]
        public int GetRandomValue(int i)
        {
            var scaleValue = GetValueByScale(i);
            var randomValue = Random.Range(minRangeValue, maxRangeValue);
            //Debug.Log($"{scaleValue} {randomValue}");
            return Mathf.RoundToInt(scaleValue * randomValue);
        }
        [Button]
        public int GetValueByScale(int i)
        {
            return Mathf.RoundToInt(baseValue * Mathf.Pow(currentScale, i));
        }



    }
}
