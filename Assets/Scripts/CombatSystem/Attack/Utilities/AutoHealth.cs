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

        private float healthRegenDelay;
        private float healthRegenRate = 3;
        [SerializeField] private HeroCharacter entity;

        [SerializeField] private ParticalSystemsManager particalSystemsManager;
        [SerializeField] private EntityTakeDamage EntityTakeDamage;
        [SerializeField] private EntityStats EntityStats;

        private void Start()
        {
            entity = GetComponentInParent<HeroCharacter>();

            particalSystemsManager = GetComponentInParent<ParticalSystemsManager>();
            EntityTakeDamage = entity.GetEntityTakeDamage();
            EntityStats = entity.GetEntityStats();

            RegisterEvent();
        }
        private void RegisterEvent()
        {
            EntityTakeDamage.OnTakeDamage += TriggerHealth;
            EntityTakeDamage.OnDie += StopHealth;
            EntityStats = entity.GetEntityStats();
        }
        private void Update()
        {
            if (EntityStats == null) return;
            if (EntityStats.Health() >= EntityStats.MaxHealth())
            {
                if (entity.IsDead)
                {
                    entity.IsDead = false;
                    healthRegenPercent = healthRegenPercentDefault;
                    EntityTakeDamage.OnRebirth?.Invoke();
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
            EntityTakeDamage.OnTakeDamage -= TriggerHealth;
        }

        private void StopHealth()
        {
            healthRegenPercent = healthRegenPercentWhenDead;
            entity.IsDead = true;
        }

        private void TriggerHealth()
        {
            healthRegenDelay = healthRegenDelayTime;
        }
    }
}