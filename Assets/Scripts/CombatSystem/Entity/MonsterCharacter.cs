using CombatSystem.Attack.Factory;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using CombatSystem.MonsterAI;
using Core.Reward;
using Helper;
using Model.Monsters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CombatSystem.Entity
{
    [RequireComponent(typeof(DamageSlashEffect))]
    public class MonsterCharacter : EntityCharacter
    {
        [SerializeField] private MonsterNearAI monsterNearAI;
        [SerializeField] private EnemyMoving enemyMoving;

        public MonsterNearSingleAttackFactory monsterSingleAttackFactory;
        private DamageSlashEffect damageSlashEffect;
        protected EntityStateManager EntityStateManager;
        private float xRandomNoise;
        private float yRandomNoise;

        protected override void Awake()
        {
            base.Awake();
            EntityStateManager = GetComponent<EntityStateManager>();
            monsterNearAI = GetComponent<MonsterNearAI>();
            enemyMoving = GetComponent<EnemyMoving>();

            damageSlashEffect = GetComponent<DamageSlashEffect>();

            EntityStateManager.OnTakeDamage += EntityStateManagerOnTakeEntityState;
            EntityStateManager.OnDie += EntityStateManagerOnDie;
        }

        private void OnDisable()
        {
            EntityStateManager.OnTakeDamage -= EntityStateManagerOnTakeEntityState;
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
            monsterNearAI.AddPositionNoise(new Vector3(xRandomNoise, yRandomNoise));
        }

        public override void PlayHurtAnimation()
        {
            animationManager.PlayAnimation(Monster_Animator.Hurt_State);
        }

        public override void RegisterObject()
        {
            CombatEntitiesManager.Instance.AppendEntityToListByTag(this, GameTag.Enemy);
            monsterNearAI.InitializeComponents();
            attackControl.Create(monsterSingleAttackFactory);
            CreateNoise();
        }

        public override void ReleaseObject()
        {
            CombatEntitiesManager.Instance.RemoveEntityByTag(this, GameTag.Enemy);
            Destroy(gameObject);
        }
    }
}