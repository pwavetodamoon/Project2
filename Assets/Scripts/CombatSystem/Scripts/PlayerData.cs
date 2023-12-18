using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Object/ PLayer Data")]
public class PlayerData : ScriptableObject
{
    public string Id;
    //public string name;
    public int level;
    public float health;
    public float damage;
    public float attackSpeed;
    public float animationSpeed;
    public float coolDown;
    public GameObject weaponPrefab;
    public Sprite WeaponSprite;
    public AttackType playerAttackType;
    public WeaponType playerWeaponType;
}
public enum AttackType
{
    Near, Far
}
public enum WeaponType
{
    Sword, Bow
}
[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Object/ Enemy Data")]
public class Enemy : PlayerData

{
    public enum EnemyType
    { goblin, elf, monster }
    public EnemyType enemy_type;
}

