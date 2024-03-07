using NewCombat.Characters;
using System;
using System.Text;
using Leveling_System;
using NewCombat.Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;

namespace NewCombat.ManagerInEntity
{
    public class EntityStateManager : MonoBehaviour, IDamageable
    {
        private IEntity entity;
        private EntityStats EntityStats;

        public  Action OnRebirth;
        public event Action OnTakeDamage;
        public event Action OnDie;
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

        private void SpawnText(float damage)
        {
            string builder = "+"+damage;
            WorldText.WorldTextPool.Instance.GetText(transform.position, builder, Color.white);
        }

        [Button]
        private void DieInvoke()
        {
            OnDie?.Invoke();
            entity.ReleaseObject();
        }

        private void Awake()
        {
            EntityStats = GetComponent<EntityStats>();
            entity = GetComponent<IEntity>();
        }



    }
}