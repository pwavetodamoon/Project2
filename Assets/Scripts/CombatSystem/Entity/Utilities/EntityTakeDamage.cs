using CombatSystem.Attack.Utilities;
using CombatSystem.Helper;
using LevelAndStats;
using Model.Hero;
using Model.Monsters;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace CombatSystem.Entity.Utilities
{
    public class EntityTakeDamage : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator_Base animation_Base;

        [SerializeField] private EntityCharacter entity;

        [SerializeField] private EntityStats EntityStats;

        public Action OnRebirth;

        public event Action OnDie;

        public event Action OnTakeDamage;
        private void Start()
        {
            entity = GetComponentInParent<EntityCharacter>();
            EntityStats = entity.GetEntityStats();
            animation_Base = entity.GetAnimatorBase();
        }

        private void OnDisable()
        {
            OnDie = null;
            OnTakeDamage = null;
        }

        private void SpawnText(float damage)
        {
            var builder = "-" + damage;
            WorldTextPool.WorldTextPool.Instance.GetText(transform.position, builder.ToString(), Color.red);
        }

        private void TakeDamage(float damage)
        {
            SpawnText(damage);
            EntityStats.DecreaseHealth(damage);
            if (EntityStats.Health() <= 0)
            {
                OnDie?.Invoke();
                return;
            }

            if (entity.EntityInAttackState() == false && EntityStats.Health() > 0) animation_Base.ChangeAnimation(AnimationType.Hurt);
            // Debug.Log($"Entity {gameObject.name} is taking damageOfEnemy: {damage} and have {EntityStats.Health()}", gameObject);
        }

        public void TakeDamage(EntityStats enemy)
        {
            OnTakeDamage?.Invoke();
            var damageOfEnemy = EntityStatsHelp.CalculatorFinalDamage(EntityStats, enemy);
            TakeDamage(damageOfEnemy);
        }
    }
}