using UnityEngine;

namespace Leveling_System
{
    public abstract class EntityStats : MonoBehaviour
    {
        [SerializeField] protected StructStats structStats;
        public bool IsCritical { get; private set; }

        public float Health()
        {
            return structStats.health;
        }

        public float MaxHealth()
        {
            return structStats.maxHealth;
        }

        public float AttackMoveDuration()
        {
            return structStats.attackMoveDuration / (1 + structStats.speed / 200);
        }

        public float AttackCoolDown()
        {
            return structStats.attackCoolDown;
        }

        public int Level()
        {
            return structStats.Level;
        }

        public void DecreaseHealth(float damage)
        {
            structStats.health -= damage;
            if (structStats.health <= 0) structStats.health = 0;
        }

        public void IncreaseHealth(float value)
        {
            structStats.health += value;

            if (structStats.health >= structStats.maxHealth) structStats.health = structStats.maxHealth;
        }

        public float GetDamage()
        {
            var damage = structStats.BaseDamage;

            IsCritical = IsCriticalHit();

            damage = IsCritical ? damage * structStats.CritDamage : damage;
            return damage;
        }

        private bool IsCriticalHit()
        {
            var rate = Random.Range(0, 100);
            return rate <= structStats.CritRate ? true : false;
        }

        public StructStats GetStructStats()
        {
            return structStats;
        }
    }
}