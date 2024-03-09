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
        private IEntity entity;
        private EntityStats EntityStats;
        private Animator_Base animation_Base;
        public Action OnRebirth;

        private void Awake()
        {
            EntityStats = GetComponent<EntityStats>();
            entity = GetComponent<IEntity>();
            animation_Base = GetComponentInChildren<Animator_Base>();
        }

        public void TakeDamage(EntityStats enemy)
        {
            OnTakeDamage?.Invoke();
            var damageOfEnemy = EntityStatsHelp.CalculatorFinalDamage(EntityStats, enemy);
            SpawnText(damageOfEnemy);
            EntityStats.DecreaseHealth(damageOfEnemy);
            if (EntityStats.Health() <= 0)
            {
                DieInvoke();
                return;
            }

            if (entity.EntityInAttackState() == false) animation_Base.ChangeAnimation(AnimationType.Hurt);
            //Debug.Log($"Entity {gameObject.name} is taking damageOfEnemy: {damageOfEnemy}", gameObject);
        }

        public event Action OnTakeDamage;
        public event Action OnDie;

        private void SpawnText(float damage)
        {
            var builder = "-" + damage;
            WorldTextPool.WorldTextPool.Instance.GetText(transform.position, builder, Color.red);
        }

        [Button]
        private void DieInvoke()
        {
            OnDie?.Invoke();
            entity.ReleaseObject();
        }
    }
}