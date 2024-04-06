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
        [SerializeField] private HealthBarDynamic healthBar;
        [SerializeField] private Vector2 HealthBarOffset;
        [SerializeField] private RewardSignal rewardSignal;
        public bool isBoss;
        protected override void Awake()
        {
            base.Awake();
            rewardSignal = GetComponentInChildren<RewardSignal>();
            entityAction.OnDie += rewardSignal.SendSignal;
            entityAction.OnDie += StopExecute;
        }
        private void Start()
        {
            this.healthBar = HealthBarManager.Instance.GetHealthBars(this);
            if (healthBar != null)
            {
                healthBar.offset = HealthBarOffset;
            }
            entityAttackControl.Create(monsterSingleAttackFactory);
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