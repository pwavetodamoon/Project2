using Leveling_System;
using NewCombat.ManagerInEntity;
using UnityEngine;

namespace NewCombat.Abilities
{
    public class AutoHealth : MonoBehaviour
    {
        private EntityStats EntityStats;
        private DamageManager damageManager;

        [SerializeField] private float healthRegenDelayTime = 7f;
        [SerializeField] private float healthRegenRateTime = 3f;
        [SerializeField] private float healthRegenPercent = 0.1f;
        private float healthRegenDelay = 0;
        private float healthRegenRate = 3;

        private void Awake()
        {
            damageManager = GetComponent<DamageManager>();
            damageManager.OnTakeDamage += TriggerHealth;

            EntityStats = GetComponent<EntityStats>();
        }

        private void OnDisable()
        {
            damageManager.OnTakeDamage -= TriggerHealth;
        }

        private void TriggerHealth()
        {
            healthRegenDelay = healthRegenDelayTime;
        }

        private void Update()
        {
            if (EntityStats == null) return;
            if (EntityStats.Health >= EntityStats.MaxHealth) return;

            if (healthRegenDelay <= 0)
            {
                if (healthRegenRate <= 0)
                {
                    var value = EntityStats.MaxHealth * healthRegenPercent;
                    EntityStats.IncreaseHealth(value);

                    healthRegenRate = healthRegenRateTime;
                }
                else
                {
                    healthRegenRate -= Time.deltaTime;
                }
            }
            else
            {
                healthRegenDelay -= Time.deltaTime;
            }
        }
    }
}