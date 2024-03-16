using System.Collections;
using System.Collections.Generic;
using CombatSystem;
using Sirenix.OdinInspector;
using UnityEngine;
[RequireComponent(typeof(TimerCounter))]
public class EnemySpawnerControl : MonoBehaviour
{
    public MonsterSpawner landMonster;
    public MonsterSpawner golemMonster;
    [SerializeField] private bool allowSpawn = true;
    [SerializeField] private int enemySpawnPerTime = 1;
    [SerializeField] private TimerCounter timerCounter;

    private void Awake()
    {
        timerCounter = GetComponent<TimerCounter>();
        timerCounter.RegisterCallback(SpawnMinions);
    }

    [Button]
    public void SpawnMinions()
    {
        if (allowSpawn == false) return;
        landMonster.SpawnMonsters(enemySpawnPerTime);
    }
    public void SpawnBoss()
    {

    }
}
