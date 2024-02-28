using System.Collections;
using System.Collections.Generic;
using Leveling_System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public int Level = 1;
    public float maxHealth = 100;
    public BaseStat health;
    public BaseStat BaseDamage;
    public BaseStat speed;

    [Header("Critical")]
    public BaseStat CritRate;
    public BaseStat CritDamage;

    [Header("Attack Settings")]
    public BaseStat attackCoolDown;

    public float attackMoveDuration = .5f;
}
