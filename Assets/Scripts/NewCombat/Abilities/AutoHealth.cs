using System;
using Leveling_System;
using NewCombat.Characters;
using NewCombat.ManagerInEntity;
using UnityEngine;

namespace NewCombat.Abilities
{
    public class AutoHealth : MonoBehaviour
    {
        private EntityStats EntityStats;
        private EntityStateManager entityStateManager;
        [SerializeField] private float healthRegenDelayTime = 7f;
        [SerializeField] private float healthRegenRateTime = 3f;
        [SerializeField] private float healthRegenPercent = 0.1f;

        [SerializeField] private float healthRegenPercentWhenDead = .2f;
        [SerializeField] private float healthRegenPercentDefault = .1f;
        private float healthRegenDelay = 0;
        private float healthRegenRate = 3;


        private HeroCharacter heroCharacter;
        private void Awake()
        {
            entityStateManager = GetComponent<EntityStateManager>();
            entityStateManager.OnTakeDamage += TriggerHealth;
            entityStateManager.OnDie += StopHealth;
            EntityStats = GetComponent<EntityStats>();
            heroCharacter = GetComponent<HeroCharacter>();
        }

        private void StopHealth()
        {
            healthRegenPercent = healthRegenPercentWhenDead;
            heroCharacter.IsDead = true;
        }


        private void OnDisable()
        {
            entityStateManager.OnTakeDamage -= TriggerHealth;
        }

        private void TriggerHealth()
        {
            healthRegenDelay = healthRegenDelayTime;
        }

        private void Update()
        {
            if (EntityStats == null) return;
            if (EntityStats.Health() >= EntityStats.MaxHealth())
            {
                if (heroCharacter.IsDead)
                {
                    heroCharacter.IsDead = false;
                    healthRegenPercent = healthRegenPercentDefault;
                    entityStateManager.OnRebirth?.Invoke();
                }
                return;
            }



            if (healthRegenDelay <= 0)
            {
                if (healthRegenRate <= 0)
                {
                    var value = EntityStats.MaxHealth() * healthRegenPercent;
                    EntityStats.IncreaseHealth(value);
                    Debug.Log("Health Value:" +value);
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