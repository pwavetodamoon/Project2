using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Factory;
using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using LevelAndStats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem.Attack.Systems
{
    public class AttackControl : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Transform attackTransform;
        private AnimationManager animatonManager;

        [ShowInInspector] private BaseSingleTargetAttack attack;
        [ShowInInspector] private AttackCounter attackCounter;
        private AttackManager attackManager;

        private EntityCharacter entityCharacter;
        private EntityStats EntityStats;


        private void Awake()
        {
            entityCharacter = GetComponent<EntityCharacter>();
            attackManager = GetComponent<AttackManager>();
            EntityStats = GetComponent<EntityStats>();
            animatonManager = GetComponent<AnimationManager>();
        }

        private void Update()
        {
            if (!CanNotRunAttackTimer()) RunAttackTimer();
        }


        [Button]
        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private bool CanNotRunAttackTimer()
        {
            return entityCharacter == null || attackCounter == null || attack == null;
        }

        private void RunAttackTimer()
        {
            var allowCounter = attackManager.IsAllowCounter();
            var allowToExecuteAnotherAttack = attackManager.IsAllowAttack();
            var isActive = attack.IsActive;

            attackCounter.UpdateMaxCounterTime(EntityStats.AttackCoolDown());
            attackCounter.UpdateNewControlState(allowCounter, isActive, allowToExecuteAnotherAttack);
            attackCounter.CheckTimerCounter(Time.deltaTime);
        }

        public void Create(SingleAttackFactory factory)
        {
            InitAttackControl(factory.CreateAttack(), new AttackCounter(3));
        }

        private void InitAttackControl(BaseSingleTargetAttack newAttack, AttackCounter newAttackCounter)
        {
            newAttack.GetReference(entityCharacter);
            if (newAttack.IsValidate == false)
            {
                Debug.LogError("Attack is not validate");
                return;
            }

            attack = newAttack;
            attackCounter = newAttackCounter;

            attack.SetAttackTransform(attackTransform);
            attackCounter.SetCoroutineRunner(this);

            attack.SetOnEndAttackCallBack(attackCounter.ResetCounter);
            attackCounter.SetAttackCallBack(attack.ExecuteAttack);
        }

        public bool IsAttacking()
        {
            if (attack == null) return false;
            return attack.IsActive;
        }

        [Button]
        public void StopExecute()
        {
            StopAllCoroutines();
        }
    }
}