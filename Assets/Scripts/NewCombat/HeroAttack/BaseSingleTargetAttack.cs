using CombatSystem;
using NewCombat.Characters;
using NewCombat.ManagerInEntity;
using System;
using System.Collections;
using Leveling_System;
using UnityEngine;

namespace NewCombat.HeroAttack
{
    [Serializable]
    public abstract class BaseSingleTargetAttack
    {
        [SerializeField] protected bool isActive;
        private Action onEndAttack;
        private float WaitTimeToFindEnemy = 0.1f;
        private bool isValidate = false;

        [Header("Entity Characters Field")]
        protected EntityCharacter entityCharacter;

        protected EntityCharacter Enemy;
        private AttackManager attackManager;
        private AnimationManager animationManager;
        protected EntityStateManager EntityStateManager;

        protected EntityStats EntityStats;

        protected Transform AttackTransform;

        public bool IsActive { get => isActive; }
        public bool IsValidate { get => isValidate; }

        public BaseSingleTargetAttack()
        { }

        public virtual void GetReference(EntityCharacter newEntityCharacter, AnimationManager _animationManager,
            AttackManager _attackManager, Transform attackTransform = null)
        {
            entityCharacter = newEntityCharacter;
            EntityStats = entityCharacter.GetComponent<EntityStats>();
            animationManager = _animationManager;
            attackManager = _attackManager;
            AttackTransform = attackTransform;
            isValidate = true;
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
            if (!IsEnemyAlive())
            {
                return false;
            }
            Enemy = CombatEntitiesManager.Instance.GetEntityTransformByTag(entityCharacter.transform, GetEnemyTag());
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
            yield return StartBehavior();
            ResetStateAndCounter();
        }

        protected virtual void CauseDamage()
        {
            if (Enemy == null)
            {
                Debug.Log(Enemy);
            }
            if (Enemy.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(EntityStats);
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
        }

        protected abstract string GetEnemyTag();

        protected void PlayAnimation(Enum AnimationEnum)
        {
            if (animationManager == null) return;
            animationManager.PlayAnimation(AnimationEnum);
        }

        protected float GetAnimationLength(Enum AnimationEnum)
        {
            if (animationManager == null) return 0;
            return animationManager.GetAnimationLength(AnimationEnum);
        }
    }
}