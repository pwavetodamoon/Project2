using System;
using UnityEngine;

namespace LevelAndStats
{
    [Serializable]
    public struct StructStats
    {
        public int Level;
        public float maxHealth;
        public float health;
        public float BaseDamage;
        public float speed;

        [Header("Critical")] public float CritRate;

        public float CritDamage;

        [Header("Attack Settings")] public float attackCoolDown;

        public float attackMoveDuration;
    }
}