using CombatSystem.Entity;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelAndStats
{
    public class EntityStats : MonoBehaviour
    {
        [SerializeField] EntityAction entityAction;
        [SerializeField] protected StructStats structStats;
        private float GetPercentHealth => structStats.health / structStats.maxHealth;

        public bool IsCritical { get; private set; }

        private bool IsCriticalHit()
        {
            var rate = Random.Range(0, 100);
            return rate <= structStats.critRate ? true : false;
        }

        protected virtual void Awake()
        {
            var entity = GetComponentInParent<EntityCharacter>();
            if (entity != null)
            {
                entityAction = entity.GetRef<EntityAction>(); 
            }
        }
        public float Speed() => structStats.speed;
        public float AttackCoolDown() => structStats.attackCoolDown;

        public float AttackMoveDuration() => structStats.attackMoveDuration / (1 + structStats.speed / 200);

        public void ChangeHealthEvent() => entityAction?.OnHealthChange?.Invoke(GetPercentHealth);

        public void DecreaseHealth(float damage)
        {
            structStats.health -= damage;
            if (structStats.health <= 0) structStats.health = 0;
            ChangeHealthEvent();
        }

        public float GetDamage()
        {
            var damage = structStats.baseDamage;

            IsCritical = IsCriticalHit();

            damage = IsCritical ? damage * structStats.critDamage : damage;
            return damage;
        }

        public StructStats GetStructStats() => structStats;

        public float Health() => structStats.health;

        public void IncreaseHealth(float value)
        {
            Debug.Log("check");
            structStats.health += value;

            if (structStats.health >= structStats.maxHealth) structStats.health = structStats.maxHealth;
            ChangeHealthEvent();
        }

        public int Level() => structStats.level;

        public float MaxHealth() => structStats.maxHealth;
        public void SetDamage(int value) => structStats.baseDamage += value;

        public void SetLevel(int value) => structStats.level = value;
        public int GetLevel() => structStats.level;
    }
}