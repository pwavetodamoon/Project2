using System.Collections;
using System.Collections.Generic;
using CombatSystem;
using Sirenix.OdinInspector;
using UnityEngine;
[RequireComponent(typeof(TimerCounter))]
public class EnemySpawnerControl : MonoBehaviour
{
    public List<MonsterSpawner> monsterSpawners;
    [SerializeField] private bool allowSpawn = true;
    [SerializeField] private int entitySpawnPerTime = 1;
    [SerializeField] private TimerCounter timerCounter;

    [Button]
    public void ClearAndStopSpawn()
    {
        allowSpawn = false;
        foreach (var item in monsterSpawners)
        {
            item.ClearMonsterAndStopSpawnOnMap();
        }
    }
    private void OnValidate()
    {
        if (timerCounter == null)
        {
            timerCounter = GetComponent<TimerCounter>();
        }
    }

    private void Awake()
    {
        timerCounter = GetComponent<TimerCounter>();
        timerCounter.RegisterCallback(SpawnMinions);

        monsterSpawners = new List<MonsterSpawner>(GetComponentsInChildren<MonsterSpawner>());
    }
    public void EnableSpawn() => allowSpawn = true;
    public void DisableSpawn() => allowSpawn = false;
    [Button]
    public void SpawnMinions()
    {
        if (allowSpawn == false) return;
        var firstSpawn = Random.Range(1, entitySpawnPerTime);

        int maxCount = entitySpawnPerTime;

        while (maxCount > 0)
        {
            var index = Random.Range(0, monsterSpawners.Count);
            var count = Random.Range(1, entitySpawnPerTime);

            Spawn(index, count);
            maxCount -= count;
        }
    }
    private void Spawn(int index, int count)
    {
        monsterSpawners[index].SpawnMultipleMonsters(count);
    }
    public void SpawnBoss()
    {

    }
}
