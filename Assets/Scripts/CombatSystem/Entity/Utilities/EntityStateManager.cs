using System;
using CombatSystem.Attack.Utilities;
using CombatSystem.Helper;
using LevelAndStats;
using Model.Hero;
using Model.Monsters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem.Entity.Utilities
{
    public class EntityStateManager : MonoBehaviour, IDamageable
    {
        private EntityCharacter entity;

        public event Action OnTakeDamage;
        public event Action OnDie;

        public EntityStats EntityStats;
        private Animator_Base animation_Base;
        public Action OnRebirth;

        private void Awake()
        {
            entity = GetComponent<EntityCharacter>();
            EntityStats = entity.GetEntityStats();
            animation_Base = entity.GetAnimatorBase();
        }

        private void OnDisable()
        {
            OnDie = null;
            OnTakeDamage = null;
        }
        public void TakeDamage(EntityStats enemy)
        {
            OnTakeDamage?.Invoke();
            var damageOfEnemy = EntityStatsHelp.CalculatorFinalDamage(EntityStats, enemy);
            TakeDamage(damageOfEnemy);

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

        private void SpawnText(float damage)
        {
            var builder = "-" + damage;
            WorldTextPool.WorldTextPool.Instance.GetText(transform.position, builder.ToString(), Color.red);
        }

        [Button]
        private void DieInvoke()
        {
            EntityStats.DecreaseHealth(120);
        }


    }
}