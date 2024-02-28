using Sirenix.OdinInspector;
using UnityEngine;

namespace Leveling_System
{
    public abstract class EntityStats : MonoBehaviour
    {
        [Header("Normal Stats")]
        [InfoBox("Damage calculator by difference by level of Entity and Enemy")]
        [SerializeField] public int Level = 1;
        [SerializeField] protected float maxHealth = 100;
        [SerializeField] protected BaseStat health;
        [SerializeField] protected BaseStat BaseDamage;
        [SerializeField] protected BaseStat speed;

        [Header("Critical")]
        [SerializeField] protected BaseStat CritRate;

        [SerializeField] protected BaseStat CritDamage;

        [Header("Attack Settings")]
        public BaseStat attackCoolDown;

        [SerializeField] protected float attackMoveDuration = .5f;
        public float Health() => health.Value;

        public float MaxHealth() => maxHealth;
        public bool IsCritical { get; private set; }
        public float AttackMoveDuration() => attackMoveDuration / (1 + speed.Value / 200);
        public float AttackCoolDown() => attackCoolDown.Value;

        public void DecreaseHealth(float damage)
        {
            health.Value -= damage;
            if (health.Value <= 0) health.Value = 0;
        }

        public void IncreaseHealth(float value)
        {
            health.Value += value;

            if (health.Value >= maxHealth) health.Value = maxHealth;
        }

        public float GetDamage()
        {
            var damage = BaseDamage.Value;

            IsCritical = IsCriticalHit();

            damage = IsCritical ? damage * CritDamage.Value : damage;
            return damage;
        }
        private bool IsCriticalHit()
        {
            var rate = UnityEngine.Random.Range(0, 100);
            return rate <= CritRate.Value ? true : false;
        }
    }
}