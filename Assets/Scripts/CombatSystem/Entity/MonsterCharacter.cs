using CombatSystem.Attack.Factory;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using Core.Reward;
using Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CombatSystem.Entity
{
    [RequireComponent(typeof(DamageSlashEffect))]
    public class MonsterCharacter : EntityCharacter
    {

        public MonsterNearSingleAttackFactory monsterSingleAttackFactory;
        protected EntityTakeDamage EntityStateManager;
        internal HealthBarDynamic healthBar;
        public Vector2 HealthBarOffset;
        protected override void Awake()
        {
            base.Awake();
            EntityStateManager = GetComponent<EntityTakeDamage>();
            EntityStateManager.OnDie += EntityStateManagerOnDie;

        }

        private void Start()
        {
            this.healthBar = HealthBarManager.Instance.GetHealthBars(this);
            if (healthBar != null)
            {
                healthBar.offset = HealthBarOffset;
            }
            attackControl.Create(monsterSingleAttackFactory);
        }

        private void OnDisable()
        {
            //Debug.Log("OnDisable",gameObject);
            EntityStateManager.OnDie -= EntityStateManagerOnDie;
        }

        private void EntityStateManagerOnDie()
        {
            GetComponent<RewardSignal>().SendSignal();
        }

        public override void RegisterObject()
        {
            base.RegisterObject();
            CombatEntitiesManager.Instance.AppendEntityToListByTag(this, GameTag.Enemy);
        }
        public override void ReleaseObject()
        {
            base.ReleaseObject();
            StopExecute();
            Debug.Log("ReleaseObject Enemy");
            CombatEntitiesManager.Instance.RemoveEntityByTag(this, GameTag.Enemy);
            //Destroy(gameObject);
        }

        public void KillMonster()
        {
            if (healthBar != null)
            {
                healthBar.Destroy();
            }
            Destroy(gameObject);
        }

    }

}