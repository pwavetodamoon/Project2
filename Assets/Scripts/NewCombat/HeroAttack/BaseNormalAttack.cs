using System;
using System.Collections;
using System.Threading;
using Characters.Monsters;
using NewCombat.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.HeroAttack
{
    [Serializable]
    public abstract class BaseNormalAttack
    {
        protected Animator_Base animator;
        public bool IsActive = false;
        public Transform AttackTransform;
        protected EntityCharacter entityCharacter;
        protected CombatCollider CombatCollider;
        public Action OnEndAttack;

       public BaseNormalAttack(EntityCharacter newEntityCharacter, Transform attackTransform = null)
        {
            entityCharacter = newEntityCharacter;
            animator = entityCharacter.transform.GetComponentInChildren<Animator_Base>();
            //attackCounter = new AttackCounter(maxCounterTime);
            CombatCollider = new CombatCollider(entityCharacter.GetComponent<BaseStats>());
            AttackTransform = attackTransform;
        }
        protected virtual IEnumerator StartBehavior()
        {
            // Method use for create behavior of attack
            // Make sure to call ResetStateAndCounter() at the end of the method
            // If enemy is null, break the method and reset IsActive to false
            yield return null;
        }
        public IEnumerator ExecuteAttack()
        {
            // This method is called when the counter is finished
            if (IsActive) yield break;
            IsActive = true;
            //StopAllCoroutines();
            yield return StartBehavior();
        }

        protected virtual void ResetStateAndCounter()
        {
            // Call in StartBehavior and at final the attack to reset state and counter
            //Debug.Log("ResetStateAndCounter");
            IsActive = false;
            entityCharacter.SetAllowAttackValue(true);
            OnEndAttack?.Invoke();
        }

        protected virtual void CauseDamage(string Tag)
        {

        }

    }
}
