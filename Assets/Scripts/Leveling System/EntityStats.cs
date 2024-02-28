using Sirenix.OdinInspector;
using UnityEngine;

namespace Leveling_System
{
    public abstract class EntityStats : MonoBehaviour
    {

        [SerializeField] protected StructStats structStats;
        public float Health() => structStats.health;

        public float MaxHealth() => structStats.maxHealth;
        public bool IsCritical { get; private set; }
        public float AttackMoveDuration() => structStats.attackMoveDuration / (1 + structStats.speed / 200);
        public float AttackCoolDown() => structStats.attackCoolDown;
        public int Level() => structStats.Level;
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
            var rate = UnityEngine.Random.Range(0, 100);
            return rate <= structStats.CritRate ? true : false;
        }
    }
}