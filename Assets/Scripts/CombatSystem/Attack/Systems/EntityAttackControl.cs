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
    public class EntityAttackControl : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Transform bowAttackTransform;
        [SerializeField] private Transform magicAttackTransform;
        [ShowInInspector] private BaseSingleTargetAttack attack;
        [ShowInInspector] private AttackCounter attackCounter;

        [SerializeField] private EntityCharacter entityCharacter;
        private EntityStats EntityStats;
        private EntityCombat attackManager;

        private void Start()
        {
            entityCharacter = GetComponentInParent<EntityCharacter>();
            attackManager = entityCharacter.GetRef<EntityCombat>();
            EntityStats = entityCharacter.GetRef<EntityStats>();
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
            if (entityCharacter == null)
            {
                Debug.LogError("EntityCharacter is null");
                Debug.Log("Please fix it");
            }
            newAttack.GetReference(entityCharacter);

            attack = newAttack;
            attackCounter = newAttackCounter;

            attack.SetAttackTransform(bowAttackTransform, magicAttackTransform);
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
            // Debug.Log("Stop Execute");
            StopAllCoroutines();
        }
    }
}