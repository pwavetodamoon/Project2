using CombatSystem.Attack.Factory;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using Core.Reward;
using Helper;
using UnityEngine;

namespace CombatSystem.Entity
{
    public class MonsterCharacter : EntityCharacter
    {
        public MonsterNearSingleAttackFactory monsterSingleAttackFactory;
        internal HealthBarDynamic healthBar;
        public Vector2 HealthBarOffset;
        [SerializeField] private RewardSignal rewardSignal;

        protected override void Awake()
        {
            base.Awake();
            entityAction.OnDie += rewardSignal.SendSignal;
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