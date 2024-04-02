using CombatSystem.Entity;
using LevelAndStats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem
{
    [RequireComponent(typeof(MonstersStatsSystem))]
    public class MonsterSpawner : MonoBehaviour
    {
        // =============================================================================
        //| Split one prefab one spawner to easy to manage + easy implement object pool|
        [Header("References")]
        [SerializeField] private MonstersStatsSystem _monstersStatsSystem;

        [SerializeField] private MonsterCharacter MonsterPrefab;
        [SerializeField] private Transform SpawnPoint1;
        [SerializeField] private Transform SpawnPoint2;

        private void Awake()
        {
            _monstersStatsSystem = GetComponent<MonstersStatsSystem>();
        }

        private MonsterCharacter SpawnMonster()
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

        public void SpawnMultipleMonsters(int spawnCount)
        {
            for (var i = 0; i < spawnCount; i++)
            {
                var enemy = SpawnMonster();
                var stats = enemy.GetRef<EnemyStats>();
                if(stats == null)
                {
                    Debug.Log("This stats of monster is null");
                }
                SetStatsToMonster(stats);
            }
        }

        private void SetStatsToMonster(EnemyStats enemyStats)
        {
            _monstersStatsSystem.SetStatsByLevel(enemyStats);
        }
        private void SetStatsForBoss(EnemyStats enemyStats)
        {
            _monstersStatsSystem.SetStatsForBoss(enemyStats);
        }

        [Button]
        public void ClearMonsterAndStopSpawnOnMap()
        {
            var list = transform.GetComponentsInChildren<MonsterCharacter>();
            for (var i = 0; i < list.Length; i++)
            {
                list[i].ReleaseObject();
                //Destroy(list[i].healthBar.gameObject);
                list[i].KillMonster();
            }
        }
        public void SpawnBoss()
        {
            var enemy = SpawnMonster();
            var stats = enemy.GetRef<EnemyStats>();
            enemy.transform.localScale = new Vector3(.5f, .5f, .5f);
            SetStatsForBoss(stats);
        }
    }
}