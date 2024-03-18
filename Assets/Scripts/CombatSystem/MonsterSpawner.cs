using System.Collections;
using CombatSystem.Entity;
using LevelAndStats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem
{
    public class MonsterSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MonstersStatsSystem _monstersStatsSystem;
        [SerializeField] private MonsterCharacter MonsterPrefab;
        [SerializeField] private Transform SpawnPoint1;
        [SerializeField] private Transform SpawnPoint2;
        private void Awake()
        {
            _monstersStatsSystem = GetComponent<MonstersStatsSystem>();
        }

        public MonsterCharacter SpawnEnemy()
        {
            if (MonsterPrefab == null) return null;
            var position = SpawnPoint1.position;
            var position1 = SpawnPoint2.position;
            var x = Random.Range(position.x, position1.x + 1);
            var y = Random.Range(position.y, position1.y);
            var spawnPosition = new Vector3(x, y, 0);
            var go = Instantiate(MonsterPrefab, spawnPosition, Quaternion.identity, transform);
            return go;
        }

        public void SpawnMonsters(int spawnCount)
        {
            for (var i = 0; i < spawnCount; i++)
            {
                var enemy = SpawnEnemy();
                SetStatsToMonster(enemy.GetComponent<EnemyStats>());
            }
        }

        private void SetStatsToMonster(EnemyStats enemyStats)
        {
            _monstersStatsSystem.SetStatsByLevel(enemyStats);

        }

        [Button]
        public void ClearMonsterAndStopSpawnOnMap()
        {
            var list = transform.GetComponentsInChildren<MonsterCharacter>();
            for (var i = 0; i < list.Length; i++) list[i].ReleaseObject();
        }

        internal void SpawnMultipleMonsters(int count)
        {
            throw new System.NotImplementedException();
        }
    }
}