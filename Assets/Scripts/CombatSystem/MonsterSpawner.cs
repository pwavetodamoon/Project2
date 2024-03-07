using System.Collections;
using NewCombat.Entity;
using UnityEngine;

namespace CombatSystem
{
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private MonsterCharacter MonsterPrefab;
        [SerializeField] private float timeCounter = 5f;
        [SerializeField] private float timeToSpawnRandomNoise = 2f;
        [SerializeField] private float spawnTime = 5f;
        [SerializeField] private int maxMonster = 3;
        [SerializeField] private Transform SpawnPoint1;
        [SerializeField] private Transform SpawnPoint2;
        [SerializeField] private bool allowSpawned;

        private void Update()
        {
            if (MonsterPrefab == null) return;
            if (timeCounter <= 0 && maxMonster > 0 && allowSpawned == false)
            {
                allowSpawned = true;
                var spawnCount = Random.Range(1, maxMonster);
                maxMonster -= spawnCount;
                StartCoroutine(SpawnMonsters(spawnCount));
            }
            else if (timeCounter > 0)
            {
                timeCounter -= Time.deltaTime;
            }
        }

        private IEnumerator SpawnMonsters(int spawnCount)
        {
            for (var i = 0; i < spawnCount; i++)
            {
                SpawnMonster();
                var time = Random.Range(0.1f, 0.5f);
                yield return new WaitForSeconds(time);
            }

            timeCounter = Random.Range(spawnTime - timeToSpawnRandomNoise, spawnTime + timeToSpawnRandomNoise);
            allowSpawned = false;
        }

        private void SpawnMonster()
        {
            if (MonsterPrefab == null) return;
            var x = Random.Range(SpawnPoint1.position.x, SpawnPoint2.position.x + 1);
            var y = Random.Range(SpawnPoint1.position.y, SpawnPoint2.position.y);
            var spawnPosition = new Vector3(x, y, 0);
            var go = Instantiate(
                MonsterPrefab,
                spawnPosition,
                Quaternion.identity,
                transform);
        }

        public void SetMaxSpawnCount(int count)
        {
            maxMonster = count;
        }

        public void ClearMonsterAndStopSpawnOnMap()
        {
            var list = transform.GetComponentsInChildren<MonsterCharacter>();
            for (var i = 0; i < list.Length; i++) list[i].ReleaseObject();
        }
    }
}