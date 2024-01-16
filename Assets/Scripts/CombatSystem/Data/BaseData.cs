using UnityEngine;

namespace CombatSystem.Data
{
    public class BaseData : ScriptableObject
    {
        // TODO: Split monster data in game and monster data in scriptable object
        // NOTE: Monster data in game will be changed in game, monster data in scriptable object will be changed in editor
        // EnemyData just use like a template for monster data in game, and monster stats will different in each monster

        // TODO: Make a properties Level for player and monster (maybe).
        // With player, stats will increase by player level
        public new string name;
        public string Id;
        public float health;
        public float damage;

        public float attackTime;
        public float animationTime;
        public float timeCoolDown;
        public GameObject prefab;
        public GameObject weaponPrefab;
        public Transform Base;
        public AttackTypeEnum AttackType;
    }

    public enum AttackTypeEnum
    {
        Near, Far
    }
}