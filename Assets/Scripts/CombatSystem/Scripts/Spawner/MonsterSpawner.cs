using Background;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace CombatSystem.Scripts.Spawner
{
    public class MonsterSpawner : MonoBehaviour
    {
        public GameObject MonsterPrefab;
        public float timeCounter = 5f;
        public float timeToSpawnRandomNoise = 2f;
        public float spawnTime = 5f;
        public int maxMonster = 3;
        public Transform SpawnPoint1;
        public Transform SpawnPoint2;
        public MapBackground mapBackground;
        bool allowSpawned = false;
        private void Update()
        {
            if (timeCounter <= 0 && maxMonster > 0 && allowSpawned == false)
            {
                allowSpawned = true;
                var spawnCount = Random.Range(1, maxMonster);
                maxMonster -= spawnCount;
                Debug.Log(spawnCount);
                StartCoroutine( SpawnMonsters(spawnCount));
               
            }
            else
            {
                timeCounter -= Time.deltaTime;
            }
        }
        private IEnumerator SpawnMonsters(int spawnCount)
        {
            for (int i = 0; i < spawnCount; i++)
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
            float x = Random.Range(SpawnPoint1.position.x, SpawnPoint2.position.x + 1);
            float y = Random.Range(SpawnPoint1.position.y, SpawnPoint2.position.y);
            var spawnPosition = new Vector3(x, y, 0);

            var go = Instantiate(MonsterPrefab, spawnPosition, Quaternion.identity);
        }
    }
}