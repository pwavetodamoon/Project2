using UnityEngine;

namespace CombatSystem.Scripts.Spawner
{
    public class MonsterSpawner : MonoBehaviour
    {
        public GameObject MonsterPrefab;
        public float timeToSpawn = 5f;
        public float timeToSpawnRandomNoise = 2f;

        private void Update()
        {
            timeToSpawn -= Time.deltaTime;
            if (timeToSpawn <= 0)
            {
                SpawnMonster();
                timeToSpawn = 10f;
            }
        }
        private void SpawnMonster()
        {
            if(MonsterPrefab == null) return;
            var go = Instantiate(MonsterPrefab, transform.position, Quaternion.identity);
        }
    }
}