using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace NewCombat.ManagerInEntity
{
    public class EntityStats : MonoBehaviour
    {
        [Header("Normal Stats")]
        public float FakeHealth = 100;

        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float health = 100;
        [SerializeField] private float BaseDamage = 10;
        [SerializeField] private float speed = 1;
        [SerializeField] private float armor = 4;

        [Header("Critical")]
        [SerializeField] private float CritRate;

        [SerializeField] private float CritDamage = 1.5f;

        [Header("Attack Settings")]
        public float AttackCoolDown = 3f;

        [SerializeField] private float attackMoveDuration = .5f;

        [Header("Level")]
        [SerializeField] private float Level = 1;

        [Header("Constant Value")]
        private float DamageScalePerLevel = 1.5f;

        public bool IsDead => FakeHealth <= 0;

        //private const float amorResitent = 100;
        public float Health => health;

        public float MaxHealth => maxHealth;
        public float ActualDamage => GetDamage();

        public float AttackMoveDuration => attackMoveDuration / (1 + speed / 200);

        // Ex: 0.5 / (1 + 1 / 200) = 0.5 / 1.005 = 0.49751243781
        // Ex: 0.5 / (1 + 2 / 200) = 0.5 / 1.01 = 0.49504950495
        public bool IsCritical { get; private set; }

        public void DecreaseHealth(float damage)
        {
            health -= CalculatorFinalDamage(damage);
            if (health <= 0) health = 0;
        }

        public void IncreaseHealth(float value)
        {
            health += value;
            FakeHealth += health;

            if (FakeHealth >= maxHealth) FakeHealth = maxHealth;
            if (health >= maxHealth) health = maxHealth;
        }

        public float CalculatorFinalDamage(float damage)
        {
            return damage * (1 - armor / (armor + 100));
        }
        [Button]
        public float CalculatorFinalDamage(float damage, float damageResistent)
        {
            return damage * (1 - damageResistent / (damageResistent + 100));
        }
        private float GetDamage()
        {
            float damage = 0;
            if (Level == 1)
                damage = BaseDamage * Level;
            else
                damage = BaseDamage * MathF.Pow(DamageScalePerLevel, Level);

            var rate = UnityEngine.Random.Range(0, 100);
            if (rate <= CritRate)
            {
                IsCritical = true;
                damage *= CritDamage;
            }
            else
            {
                IsCritical = false;
            }

            return damage;
        }

        [Button]
        public void ResetState()
        {
            health = 100;
        }

    }
}