using Leveling_System;
using NewCombat.AttackFactory;
using NewCombat.Characters;
using NewCombat.HeroAttack;
using NewCombat.ManagerInEntity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat
{
    public class AttackControl : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Transform attackTransform;

        [ShowInInspector] private BaseSingleTargetAttack attack;
        [ShowInInspector] private AttackCounter attackCounter;

        private EntityCharacter entityCharacter;
        private AttackManager attackManager;
        private AnimationManager animatonManager;
        private EntityStats EntityStats;


        private void Awake()
        {
            entityCharacter = GetComponent<EntityCharacter>();
            attackManager = GetComponent<AttackManager>();
            EntityStats = GetComponent<EntityStats>();
            animatonManager = GetComponent<AnimationManager>();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void Update()
        {
            if (!CanNotRunAttackTimer())
            {
                RunAttackTimer();
            }
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
            newAttack.GetReference(entityCharacter, animatonManager, attackManager, null);
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
    }
}