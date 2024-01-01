using UnityEngine;

public class BaseData : ScriptableObject
{ 
    public new string name;
    public string Id;
    public float health;
    public float damage;

    public float attackTime;
    public float animationTime;
    public float timeCoolDown;
    public GameObject weaponPrefab;
    public Transform Base;
    public AttackTypeEnum AttackType;

}
    public enum AttackTypeEnum
    {
        Near, Far
    }

