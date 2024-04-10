using CombatSystem.Attack.Utilities;
using CombatSystem.Helper;
using LevelAndStats;
using Model.Hero;
using Model.Monsters;
using System;
using Unity.Mathematics;
using UnityEngine;

namespace CombatSystem.Entity.Utilities
{
    public class EntityTakeDamage : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator_Base animation_Base;

        [SerializeField] private EntityCharacter entity;

        [SerializeField] private EntityStats EntityStats;

        [SerializeField] private EntityAction entityAction;

        private void Start()
        {
            entity = GetComponentInParent<EntityCharacter>();
            EntityStats = entity.GetRef<EntityStats>();
            animation_Base = entity.GetRef<Animator_Base>();
            entityAction = entity.GetRef<EntityAction>();
        }


        private void SpawnText(float damage)
        {
            var builder = "-" + damage;
            WorldTextPool.WorldTextPool.Instance.GetText(transform.position, builder.ToString(), Color.red);
        }

        private void TakeDamage(float damage)
        {
            damage = Mathf.Round(damage);
            SpawnText(damage);
            EntityStats.DecreaseHealth(damage);
            if (EntityStats.Health() <= 0)
            {
                //Debug.Log($"{entity.name} is out of health ", entity.gameObject);
                entityAction.OnDie?.Invoke();
                return;
            }

            if (entity.EntityInAttackState() == false && EntityStats.Health() > 0) animation_Base.ChangeAnimation(AnimationType.Hurt);
            // Debug.Log($"Entity {gameObject.name} is taking damageOfEnemy: {damage} and have {EntityStats.Health()}", gameObject);
        }

        public void TakeDamage(EntityStats enemy)
        {
            entityAction.OnTakeDamage?.Invoke();
            var damageOfEnemy = EntityStatsHelp.CalculatorFinalDamage(EntityStats, enemy);
            TakeDamage(damageOfEnemy);
        }
    }
}