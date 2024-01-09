using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CharactersBase : MonoBehaviour
{
    [SerializeField] public BaseData data;
    [SerializeField] protected float timeCounter;
    [SerializeField] public HealthBase health;
    [SerializeField] protected AttackTypeEnum type;
    [SerializeField] protected bool attacking = false;
    [SerializeField] protected Animator_Base animator;
    protected List<ICommand> AttackCommands = new List<ICommand>();
    public AttackBase normalAttack;

    [Button]
    public virtual void ChangeComponent()
    {
        if (GetComponent<AttackBase>() != null)
        {
            DestroyImmediate(GetComponent<AttackBase>());
        }
        if (type == AttackTypeEnum.Near)
        {
            transform.AddComponent<ShortRange>();
        }
        if (type == AttackTypeEnum.Far)
        {
            transform.AddComponent<LongRange>();
        }
        normalAttack = GetComponent<AttackBase>();
        if (normalAttack != null)
        {
            normalAttack.Init(data);
        }
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
    protected abstract void InitCommandList();
    [Button]
    public abstract void Attack();
    protected IEnumerator MoveToEnemyCoroutine(Vector2 enemyPos, float time = 1)
    {
        yield return transform.DOMove(enemyPos, time).WaitForCompletion();
    }
}