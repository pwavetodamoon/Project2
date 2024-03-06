using NewCombat.Characters;
using System;
using Leveling_System;
using NewCombat.Helper;
using Sirenix.OdinInspector;
using UnityEngine;

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

            var damageOfEnemy = EntityStatsHelp.CalculatorFinalDamage(EntityStats, enemy);
            EntityStats.DecreaseHealth(damageOfEnemy);
            OnTakeDamage?.Invoke();
            var str = "+" + damageOfEnemy;
            WorldText.WorldTextPool.Instance.GetText(transform.position, str);

            if (EntityStats.Health() <= 0)
            {
                OnDie?.Invoke();
                entity.ReleaseObject();
                return;
            }

            if (entity.EntityInAttackState() == false) entity.PlayHurtAnimation();
            Debug.Log($"Entity {gameObject.name} is taking damageOfEnemy: {damageOfEnemy}", gameObject);
        }

        [Button]
        private void TestOnDie()
        {
            EntityStats.DecreaseHealth(EntityStats.MaxHealth());
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