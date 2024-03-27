using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using Effects.Skill;
using LevelAndStats;
using UnityEngine;

namespace CombatSystem.Attack.Utilities
{
    public class AutoHealth : MonoBehaviour
    {
        [SerializeField] private float healthRegenDelayTime = 7f;
        [SerializeField] private float healthRegenRateTime = 3f;
        [SerializeField] private float healthRegenPercent = 0.1f;

        [SerializeField] private float healthRegenPercentWhenDead = .2f;
        [SerializeField] private float healthRegenPercentDefault = .1f;
        private EntityStateManager entityStateManager;
        private EntityStats EntityStats;
        private float healthRegenDelay;
        private float healthRegenRate = 3;
        private ParticalSystemsManager particalSystemsManager;

        private HeroCharacter heroCharacter;

        private void Awake()
        {
            particalSystemsManager = GetComponent<ParticalSystemsManager>();
            heroCharacter = GetComponent<HeroCharacter>();
            entityStateManager = GetComponent<EntityStateManager>();
            entityStateManager.OnTakeDamage += TriggerHealth;
            entityStateManager.OnDie += StopHealth;
            EntityStats = heroCharacter.GetEntityStats();
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
                    Debug.Log("Health Value:" + value);
                    healthRegenRate = healthRegenRateTime;
                    WorldTextPool.WorldTextPool.Instance.GetText(transform.position, $"+{value}", Color.green);
                    particalSystemsManager.FindAndPlayEffect(EffectSkillsEnum.HealthEffect);
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


        private void OnDisable()
        {
            entityStateManager.OnTakeDamage -= TriggerHealth;
        }

        private void StopHealth()
        {
            healthRegenPercent = healthRegenPercentWhenDead;
            heroCharacter.IsDead = true;
        }

        private void TriggerHealth()
        {
            healthRegenDelay = healthRegenDelayTime;
        }
    }
}