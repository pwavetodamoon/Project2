using CombatSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
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
        if (allowSpawn == false || monsterSpawners.Count == 0) return;
        var spawnerIndex = Random.Range(0, monsterSpawners.Count);
        monsterSpawners[spawnerIndex].SpawnMultipleMonsters(entitySpawnPerTime);
    }

    public void SpawnBoss()
    {
    }
}