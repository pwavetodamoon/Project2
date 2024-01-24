using System.Collections;
using Characters.Monsters;
using CombatSystem;
using UnityEngine;

namespace NewCombat.Characters
{
    [RequireComponent(typeof(BaseStats))]
    public abstract class EntityCharacter : MonoBehaviour, IDamageable
    {
        protected Animator_Base Animator;
        protected BaseStats BaseStats;
        protected AttackControl attackControl;
        protected bool allowExcuteAnotherAttack = true;
        protected bool allowCounter = true;
        // Read and Set method
        public bool IsAllowAttack() => allowExcuteAnotherAttack;
        public bool IsAllowCounter() => allowCounter;
        public bool SetAllowAttackValue(bool value) => allowExcuteAnotherAttack = value;
        public bool SetCounterValue(bool value) => allowCounter = value;
        public Transform Model;

        protected virtual void Awake()
        {
            attackControl = GetComponent<AttackControl>();
            BaseStats = GetComponent<BaseStats>();
            Animator = GetComponentInChildren<Animator_Base>();
            RegisterObject();
            attackControl.entityCharacter = this;

        }
        protected virtual void OnDestroy()
        {
            RelaseObject();
        }
        public virtual IEnumerator TakeDamageCoroutine(float damage)
        {
            UpdateState(false);
            BaseStats.Health -= damage;

            if (BaseStats.Health <= 0)
            {
                BaseStats.Health = 0;
                Destroy(gameObject);
                yield break;
            }

            //Debug.Log("Monster is taking damage");
            yield return new WaitForSeconds(PlayHurtAnimation());
            UpdateState(true);
        }

        protected abstract float PlayHurtAnimation();
        /// <summary>
        /// Update value attack and counter state in object by parameter
        /// </summary>
        /// <param name="boolen"></param>
        protected virtual void UpdateState(bool boolen)
        {
            allowExcuteAnotherAttack = boolen;
            allowCounter = boolen;
        }
        /// <summary>
        /// Decrease health in Base Stats
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(float damage)
        {
            StartCoroutine(TakeDamageCoroutine(damage));
            Debug.Log($"Entity {gameObject.name} is taking damage: {damage}", gameObject);
        }

        protected abstract void RegisterObject();
        protected abstract void RelaseObject();
    }
}