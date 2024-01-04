using UnityEngine;



public class AttackBase : MonoBehaviour
{
    public AttackBase(BaseData data)
    {
        this.data = data;
        IsAttack = false;
    }
    protected BaseData data;
    protected bool IsAttack = false;
}

