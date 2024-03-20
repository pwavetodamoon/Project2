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
        protected EntityStateManager EntityStateManager;
        internal HealthBarDynamic healthBar;

        protected override void Awake()
        {
            base.Awake();
            EntityStateManager = GetComponent<EntityStateManager>();
            EntityStateManager.OnDie += EntityStateManagerOnDie;

        }

        private void Start()
        {
            healthBar = HealthBarManager.Instance.GetHealthBars(this);

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
            CombatEntitiesManager.Instance.RemoveEntityByTag(this, GameTag.Enemy);
            //Destroy(gameObject);
        }

        public void KillMonster()
        {
            Destroy(gameObject);
        }

    }

}