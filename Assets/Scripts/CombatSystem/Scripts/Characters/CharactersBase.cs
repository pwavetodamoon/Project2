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
    protected bool attacking = false;

    public AttackBase normalAttack;

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
        var attackBase = GetComponent<AttackBase>();
        if (attackBase != null)
        {
            attackBase.Init(data);
        }

    }
    public void TakeDamage(float damage)
    {
        if(health != null)
        {
            health.ChangeHealth(damage);
        }
    }
    protected virtual void Attack()
    {

    }
    protected abstract IEnumerator TimeCount();

}
