using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CharactersBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public BaseData data;
    [SerializeField] protected CharactersBase enemy;
    [SerializeField] public HealthBase health;
    [SerializeField] protected Animator_Base animator;
    [SerializeField] protected ActionSequence attackSequence;

    [Header("Settings")]
    [SerializeField] protected float timeCounter;
    [SerializeField] protected int enemyIndex = 0;
    [SerializeField] protected bool allowAction = true;
    [SerializeField] protected bool attacking = false;

    protected List<ICommand> AttackCommands = new List<ICommand>();


    public Transform SlotPosition;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator_Base>();
        health = GetComponent<HealthBase>();
    }
    protected virtual void Start()
    {
        health.Setup(data);
        InitCommandList();
    }
    [Button]
    public abstract void Attack();
    [Button]
    public virtual void CheckEnemy()
    {
        var newEnemy = CombatManager.GetEnemyPosition?.Invoke(enemyIndex);
        if (newEnemy == null)
        {
            return;
        }
        if (enemy == null)
        {
            enemy = newEnemy;
        }
    }


    protected virtual void AttackMechanism()
    {
        if (allowAction == false) return;
        if (enemy == null)
        {
            CheckEnemy();
            return;
        }
        if (timeCounter <= 0 && !attacking)
        {
            attacking = true;
            Attack();
        }
    }
    protected virtual void AttackEnemy()
    {
        if (enemy == null)
        {
            Debug.Log("Enemy is null", gameObject);
            return;
        }
        enemy.TakeDamage(data.damage);
    }

    protected abstract void InitCommandList();

    protected IEnumerator MoveToEnemyCoroutine(Vector2 enemyPos, float time = 1)
    {
        yield return transform.DOMove(enemyPos, time).WaitForCompletion();
    }

    protected virtual void ResetState()
    {
        attacking = false;
        timeCounter = data.timeCoolDown;
    }


    public float GetHealth()
    {
        if (health != null)
        {
            return health.GetHealth();
        }
        return 0;
    }

    public void TakeDamage(float damage)
    {
        if (health != null)
        {
            Debug.Log(gameObject.name + " take damage " + damage);
            health.ChangeHealth(damage);
        }
    }
}