using System;
using LevelAndStats;
using NewCombat.Attack.Utilities;
using NewCombat.Helper;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.Entity.Utilities
{
    public class EntityStateManager : MonoBehaviour, IDamageable
    {
        private IEntity entity;
        private EntityStats EntityStats;

        public Action OnRebirth;

        private void Awake()
        {
            EntityStats = GetComponent<EntityStats>();
            entity = GetComponent<IEntity>();
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

            if (entity.EntityInAttackState() == false) entity.PlayHurtAnimation();
            //Debug.Log($"Entity {gameObject.name} is taking damageOfEnemy: {damageOfEnemy}", gameObject);
        }

        public event Action OnTakeDamage;
        public event Action OnDie;

        private void SpawnText(float damage)
        {
            var builder = "+" + damage;
            WorldTextPool.WorldTextPool.Instance.GetText(transform.position, builder, Color.white);
        }

        [Button]
        private void DieInvoke()
        {
            OnDie?.Invoke();
            entity.ReleaseObject();
        }
    }
}