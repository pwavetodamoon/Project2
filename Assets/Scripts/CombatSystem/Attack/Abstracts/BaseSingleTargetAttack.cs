using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using Helper;
using LevelAndStats;
using Model.Monsters;
using System;
using System.Collections;
using UnityEngine;

namespace CombatSystem.Attack.Abstracts
{
    [Serializable]
    public abstract class BaseSingleTargetAttack : MonoBehaviour
    {
        // [SerializeField] protected bool isActive;

        // [SerializeField] protected Transform BowAttackTransform;
        // [SerializeField] protected Transform MagicAttackTransform;
        // [SerializeField] protected EntityStats EntityStats;
        // [SerializeField] protected EntityCharacter Enemy;
        // [SerializeField] protected Animator_Base animator;
        // [SerializeField] protected EntityCombat IAttackerCounter;

        // [Header("Entity Characters Field")] protected EntityCharacter entityCharacter;

        // private float WaitTimeToFindEnemy = 0.1f;

        // [SerializeField] private EntityCombat entityCombat;
        // public bool IsActive => isActive;
        // public bool IsValidate { get; private set; }
        // private Action onEndAttack;

        // public virtual void GetReference(EntityCharacter newEntityCharacter, Transform attackTransform = null)
        // {
        //     entityCharacter = newEntityCharacter;
        //     EntityStats = entityCharacter.GetRef<EntityStats>();
        //     entityCombat = entityCharacter.GetRef<EntityCombat>();
        //     animator = entityCharacter.GetRef<Animator_Base>();
        //     BowAttackTransform = attackTransform;
        //     IsValidate = true;
        // }

        // protected abstract string GetEnemyTag();

        // protected abstract IEnumerator StartBehavior();

        // public void SetOnEndAttackCallBack(Action callback) => onEndAttack = callback;

        // public void SetAttackTransform(Transform bowAttackTransform, Transform magicAttackTransform)
        // {
        //     BowAttackTransform = bowAttackTransform;
        //     MagicAttackTransform = magicAttackTransform;
        // }


        // private bool IsEnemyAlive()
        // {
        //     // Debug.Log($"{entityCharacter.name}:Ask IsEnemyAlive: {CombatEntitiesManager.Instance.IsHaveEntityHaveTagAlive(GetEnemyTag())}");
        //     return CombatEntitiesManager.Instance.IsHaveEntityHaveTagAlive(GetEnemyTag());
        // }

        // protected void PlayAnimation(Enum AnimationEnum)
        // {
        //     if (animator == null) return;
        //     animator.ChangeAnimation(AnimationEnum);
        // }

        // protected float GetAnimationLength(Enum AnimationEnum)
        // {
        //     if (animator == null) return 0;
        //     return animator.GetAnimationLength(AnimationEnum);
        // }

        // public IEnumerator ExecuteAttack()
        // {
        //     // Debug.Log("Coroutine ExecuteAttack");
        //     if (isActive || !TryFindEnemy())
        //     {
        //         yield return WaitAndContinue();
        //         yield break;
        //     }

        //     yield return PerformAttack();
        // }

        // private bool TryFindEnemy()
        // {
        //     if (!IsEnemyAlive()) return false;
        //     Enemy = CombatEntitiesManager.Instance.GetEntityTransformByTag(entityCharacter.transform, GetEnemyTag());
        //     Debug.Log(Enemy);
        //     // Debug.Log($"{entityCharacter.name}: Try find enemy:", Enemy);
        //     return true;
        // }

        // private IEnumerator WaitAndContinue()
        // {
        //     yield return new WaitForSeconds(WaitTimeToFindEnemy);
        // }

        // private IEnumerator PerformAttack()
        // {
        //     isActive = true;
        //     // error is here
        //     entityCombat.SetAllowExecuteAttackValue(false);
        //     if (CanExecuteAttack())
        //     {
        //         yield return StartBehavior();
        //     }
        //     ResetStateAndCounter();
        // }

        // protected virtual void ResetStateAndCounter()
        // {
        //     // Call in StartBehavior and at final the attack to reset state and counter
        //     isActive = false;
        //     entityCombat.SetAllowExecuteAttackValue(true);
        //     onEndAttack?.Invoke();
        //     IAttackerCounter?.DecreaseAttackerCount(EntityStats);
        // }

        // public bool CanExecuteAttack()
        // {
        //     if (GetEnemyTag() == GameTag.Hero)
        //     {
        //         return true;
        //     }
        //     // if before attack, the enemy is out of health, then return false
        //     // if not then increase the attacker count and return true
        //     IAttackerCounter = Enemy.GetRef<EntityCombat>();
        //     //TODO: Check if the enemy is out of health
        //     IAttackerCounter.Check(EntityStats, GetEnemyTag());

        //     return false;
        // }

        // protected virtual void CheckBeforeAttack()
        // {
        //     if (Enemy == null)
        //     {
        //         Debug.Log(Enemy);
        //         return;
        //     }
        //     if (Enemy.TryGetComponent(out IDamageable damageable))
        //     {
        //         damageable?.TakeDamage(EntityStats);
        //     }
        // }
    }
}