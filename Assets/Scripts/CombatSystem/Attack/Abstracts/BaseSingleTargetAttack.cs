using System;
using System.Collections;
using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using Helper;
using LevelAndStats;
using Model.Monsters;
using UnityEngine;

namespace CombatSystem.Attack.Abstracts
{
    [Serializable]
    public abstract class BaseSingleTargetAttack
    {
        [SerializeField] protected bool isActive;
        private AttackManager attackManager;

        protected Transform AttackTransform;
        protected EntityStats EntityStats;
        protected EntityCharacter Enemy;
        protected Animator_Base animator;
        [Header("Entity Characters Field")] protected EntityCharacter entityCharacter;

        private Action onEndAttack;
        private float WaitTimeToFindEnemy = 0.1f;

        public bool IsActive => isActive;
        public bool IsValidate { get; private set; }

        public virtual void GetReference(EntityCharacter newEntityCharacter, Transform attackTransform = null)
        {
            entityCharacter = newEntityCharacter;
            EntityStats = entityCharacter.GetComponent<EntityStats>();
            attackManager = entityCharacter.GetComponent<AttackManager>();
            animator = entityCharacter.GetComponentInChildren<Animator_Base>();
            AttackTransform = attackTransform;
            IsValidate = true;
        }

        public void SetOnEndAttackCallBack(Action callback)
        {
            onEndAttack = callback;
        }

        public void SetAttackTransform(Transform newTransform)
        {
            AttackTransform = newTransform;
        }

        public IEnumerator ExecuteAttack()
        {
            if (isActive || !TryFindEnemy())
            {
                yield return WaitAndContinue();
                yield break;
            }

            yield return PerformAttack();
        }

        private bool TryFindEnemy()
        {
            if (!IsEnemyAlive()) return false;
            Enemy = CombatEntitiesManager.Instance.GetEntityTransformByTag(entityCharacter.transform, GetEnemyTag());
            //Debug.Log("Try find enemy",Enemy);
            return true;
        }

        private IEnumerator WaitAndContinue()
        {
            yield return new WaitForSeconds(WaitTimeToFindEnemy);
        }

        private IEnumerator PerformAttack()
        {
            isActive = true;
            attackManager.SetAllowExecuteAttackValue(false);
            if (CanExecuteAttack())
            {
                yield return StartBehavior();
                ResetStateAndCounter();
            }
        }

        protected virtual void CauseDamage()
        {
            if (Enemy == null)
            {
                Debug.Log(Enemy);
                return;
            }
            if (Enemy.TryGetComponent(out IDamageable damageable))
            {
                damageable?.TakeDamage(EntityStats);
            }

        }

        private bool IsEnemyAlive()
        {
            return CombatEntitiesManager.Instance.IsHaveEntityHaveTagAlive(GetEnemyTag());
        }

        // use for create attack behavior
        protected abstract IEnumerator StartBehavior();

        protected virtual void ResetStateAndCounter()
        {
            // Call in StartBehavior and at final the attack to reset state and counter
            isActive = false;
            attackManager.SetAllowExecuteAttackValue(true);
            onEndAttack?.Invoke();
            IAttackerCounter?.DecreaseAttackerCount(EntityStats);
        }

        protected abstract string GetEnemyTag();

        protected void PlayAnimation(Enum AnimationEnum)
        {
            if (animator == null) return;
            animator.ChangeAnimation(AnimationEnum);
        }

        protected float GetAnimationLength(Enum AnimationEnum)
        {
            if (animator == null) return 0;
            return animator.GetAnimationLength(AnimationEnum);
        }
        public bool CanExecuteAttack()
        {
            if (GetEnemyTag() == GameTag.Hero)
            {
                return true;
            }
            IAttackerCounter = Enemy.GetComponent<IAttackerCounter>();
            if (IAttackerCounter.CanAttack())
            {
                IncreaseAttackerCount();
                if (IAttackerCounter.CanAttack() == false)
                {
                    CombatEntitiesManager.Instance.RemoveEntityByTag(Enemy, GetEnemyTag());
                }
                return true;
            }
            return false;
        }
        protected IAttackerCounter IAttackerCounter;

        protected void IncreaseAttackerCount()
        {
            //Debug.Log("+1", entityCharacter.gameObject);
            IAttackerCounter?.IncreaseAttackerCount(EntityStats);
        }

    }
}