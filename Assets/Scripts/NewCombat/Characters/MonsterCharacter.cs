using Characters.Monsters;
using CombatSystem;
using CombatSystem.Scripts;
using NewCombat.AttackFactory;
using NewCombat.Helper;
using NewCombat.HeroAttack;
using NewCombat.ManagerInEntity;
using NewCombat.MonsterAI;
using System;
using DropItem;
using Helper;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NewCombat.Characters
{
    [RequireComponent(typeof(DamageSlashEffect))]
    public class MonsterCharacter : EntityCharacter
    {
        [SerializeField] private MonsterNearAI monsterNearAI;
        [SerializeField] private EnemyMoving enemyMoving;

        public MonsterNearSingleAttackFactory monsterSingleAttackFactory;
        private DamageSlashEffect damageSlashEffect;
        protected DamageManager damageManager;
        private float xRandomNoise = 0;
        private float yRandomNoise = 0;

        protected override void Awake()
        {
            base.Awake();
            damageManager = GetComponent<DamageManager>();
            monsterNearAI = GetComponent<MonsterNearAI>();
            enemyMoving = GetComponent<EnemyMoving>();
            
            damageSlashEffect = GetComponent<DamageSlashEffect>();

            damageManager.OnTakeDamage += DamageManager_OnTakeDamage;
            damageManager.OnDie += DamageManager_OnDie;
        }

        private void DamageManager_OnDie()
        {
            GetComponent<RewardSignal>().SendSignal();
        }

        private void DamageManager_OnTakeDamage()
        {
            damageSlashEffect.TriggerFlashEffect();
        }

        private void OnDisable()
        {
            damageManager.OnTakeDamage -= DamageManager_OnTakeDamage;
            damageManager.OnDie -= DamageManager_OnDie;
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