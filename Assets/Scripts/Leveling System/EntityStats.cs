using Sirenix.OdinInspector;
using UnityEngine;

namespace Leveling_System
{
    public class EntityStats : MonoBehaviour
    {
        [Header("Normal Stats")]
        [InfoBox("Damage calculator by difference by level of Entity and Enemy")]
        [SerializeField] public int Level = 1;
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private BaseStat health;
        [SerializeField] private BaseStat BaseDamage;
        [SerializeField] private BaseStat speed;

        [Header("Critical")]
        [SerializeField] private BaseStat CritRate;

        [SerializeField] private BaseStat CritDamage;

        [Header("Attack Settings")]
        private BaseStat attackCoolDown;

        [SerializeField] private float attackMoveDuration = .5f;
        private void Awake()
        {
            // For GetItemInPool
            health.Value = maxHealth;
            BaseDamage = new BaseStat(10);
            speed = new BaseStat(1);
            CritRate = new BaseStat(1.5f);
            CritDamage = new BaseStat(1.5f);
            attackCoolDown = new BaseStat(3f);
        }
        public float Health => health.Value;

        public float MaxHealth => maxHealth;
        public bool IsCritical { get; private set; }

        public float AttackMoveDuration => attackMoveDuration / (1 + speed.Value / 200);
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
        [Button]
        public void ResetState()
        {
            health.Value = 100;
        }
      
    }
}