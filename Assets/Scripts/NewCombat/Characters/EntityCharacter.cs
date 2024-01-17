using System.Collections;
using System.Collections.Generic;
using Characters.Monsters;
using NewCombat.Characters;
using UnityEngine;

[RequireComponent(typeof(BaseStats))]
public abstract class EntityCharacter : MonoBehaviour , IDamageable
{
    public Animator_Base Animator;
    public BaseStats BaseStats;
    protected virtual void Awake()
    {
        BaseStats = GetComponent<BaseStats>();
        Animator = GetComponentInChildren<Animator_Base>();
    }
    public virtual void TakeDamage(float damage)
    {
        if(BaseStats == null)
            BaseStats = GetComponent<BaseStats>();
        BaseStats.Health -= damage;
        if(BaseStats.Health <= 0)
        {
            BaseStats.Health = 0;
            Debug.Log("Monster is dead");
        }
    }
}
