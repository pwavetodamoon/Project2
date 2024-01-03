using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersBase : MonoBehaviour
{
    [SerializeField] public BaseData data;
    [SerializeField] protected float timeCounter;

    [SerializeField] protected AttackTypeEnum type;
    public AttackBase normalAttack;
    public virtual void ChangeComponent()
    {

    }
    protected virtual void Attack()
    {

    }
    protected virtual IEnumerator TimeCount()
    {
        yield return null;
    }
}
