using NewCombat.Characters;
using System;
using Leveling_System;
using NewCombat.Helper;
using UnityEngine;

namespace NewCombat.ManagerInEntity
{
    public class DamageManager : MonoBehaviour, IDamageable
    {
        private IEntity entity;
        private EntityStats EntityStats;

        public event Action OnTakeDamage;
        public event Action OnDie;
        public void TakeDamage(EntityStats enemy)
        {

            var damageOfEnemy = EntityStatsHelp.CalculatorFinalDamage(EntityStats, enemy);
            EntityStats.DecreaseHealth(damageOfEnemy);
            OnTakeDamage?.Invoke();
            WorldTextPool.WorldTextPool.Instance.GetCombatTxt(transform.position, damageOfEnemy.ToString());

            if (EntityStats.Health() <= 0)
            {
                OnDie?.Invoke();
                entity.ReleaseObject();
                return;
            }

            if (entity.EntityAreNotInAttackState()) entity.PlayHurtAnimation();
            Debug.Log($"Entity {gameObject.name} is taking damageOfEnemy: {damageOfEnemy}", gameObject);
        }

        private void Awake()
        {
            EntityStats = GetComponent<EntityStats>();
            entity = GetComponent<IEntity>();
        }



    }
}