using System.Collections;
using System.Collections.Generic;
using CombatSystem.Attack.Utilities;
using LevelAndStats;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class HeroSkill : MonoBehaviour
{
    public Animator animator;
    protected IAttackerCounter attacker;
    public EntityStats entityStats;

    public Vector2 size = Vector2.one;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.transform.position, size);
    }
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        entityStats = GetComponent<EntityStats>();
    }
    public abstract void DealDamage();
    public abstract void IncreaseAttacker(); // Use for stop enemy moving
    public abstract void DecreaseAttacker();

    public abstract void Destroy();
}
