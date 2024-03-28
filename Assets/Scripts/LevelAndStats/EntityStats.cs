using CombatSystem.Entity;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelAndStats
{
    public class EntityStats : MonoBehaviour
    {
        [SerializeField] protected StructStats structStats;
        [SerializeField] EntityAction entityAction;
        public bool IsCritical { get; private set; }

        public float Health() => structStats.health;

        public float MaxHealth() => structStats.maxHealth;

        public float AttackMoveDuration() => structStats.attackMoveDuration / (1 + structStats.speed / 200);

        public float AttackCoolDown() => structStats.attackCoolDown;

        public StructStats GetStructStats() => structStats;

        public float GetPercentHealth => structStats.health / structStats.maxHealth;

        public int Level() => structStats.level;

        public void SetLevel(int value) => structStats.level = value;

        public void DecreaseHealth(float damage)
        {
            structStats.health -= damage;
            if (structStats.health <= 0) structStats.health = 0;
            ChangeHealthEvent();
        }

        public void IncreaseHealth(float value)
        {
            Debug.Log("check");
            structStats.health += value;

            if (structStats.health >= structStats.maxHealth) structStats.health = structStats.maxHealth;
            ChangeHealthEvent();
        }
        public void ChangeHealthEvent() => entityAction.OnHealthChange?.Invoke(GetPercentHealth);
        public float GetDamage()
        {
            var damage = structStats.baseDamage;

            IsCritical = IsCriticalHit();

            damage = IsCritical ? damage * structStats.critDamage : damage;
            return damage;
        }

        public void SetDamage(int value) => structStats.baseDamage += value;

        private bool IsCriticalHit()
        {
            var rate = Random.Range(0, 100);
            return rate <= structStats.critRate ? true : false;
        }
    }
}