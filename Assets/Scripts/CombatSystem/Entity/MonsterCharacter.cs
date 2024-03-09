using CombatSystem.Attack.Factory;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using CombatSystem.MonsterAI;
using Core.Reward;
using Helper;
using Model.Hero;
using Model.Monsters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CombatSystem.Entity
{
    [RequireComponent(typeof(DamageSlashEffect))]
    public class MonsterCharacter : EntityCharacter
    {

        public MonsterNearSingleAttackFactory monsterSingleAttackFactory;
        private DamageSlashEffect damageSlashEffect;
        protected EntityStateManager EntityStateManager;
        private float xRandomNoise;
        private float yRandomNoise;

        protected override void Awake()
        {
            base.Awake();
            EntityStateManager = GetComponent<EntityStateManager>();

            damageSlashEffect = GetComponent<DamageSlashEffect>();

            EntityStateManager.OnDie += EntityStateManagerOnDie;

        }

        private void Start()
        {
            attackControl.Create(monsterSingleAttackFactory);
        }

        private void OnDisable()
        {
            EntityStateManager.OnDie -= EntityStateManagerOnDie;
        }

        private void EntityStateManagerOnDie()
        {
            GetComponent<RewardSignal>().SendSignal();
        }

        private void EntityStateManagerOnTakeEntityState()
        {
            damageSlashEffect.TriggerFlashEffect();
        }

        private void CreateNoise()
        {
            xRandomNoise = Random.Range(0, 0.3f);
            yRandomNoise = Random.Range(-0.3f, .3f);
        }


        public override void RegisterObject()
        {
            CombatEntitiesManager.Instance.AppendEntityToListByTag(this, GameTag.Enemy);
            CreateNoise();
        }

        public override void ReleaseObject()
        {
            CombatEntitiesManager.Instance.RemoveEntityByTag(this, GameTag.Enemy);
            Destroy(gameObject);
        }
    }
}