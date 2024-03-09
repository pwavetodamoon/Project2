using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelAndStats
{
    [Serializable]
    public struct StructStats
    {
        public int level;
        public float maxHealth;
        public float health;
        public float baseDamage;
        public float speed;
        public float critRate;
        public float critDamage;
        public float attackCoolDown;
        public float attackMoveDuration;
    }
}