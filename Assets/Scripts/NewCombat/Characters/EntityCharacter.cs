using System.Collections;
using Characters.Monsters;
using UnityEngine;

namespace NewCombat.Characters
{
    [RequireComponent(typeof(BaseStats))]
    public abstract class EntityCharacter : MonoBehaviour, IDamageable
    {
        public Animator_Base Animator;
        public BaseStats BaseStats;
        public bool allowExcuteAnotherAttack = true;
        public bool allowCounter = true;

        protected virtual void Awake()
        {
            BaseStats = GetComponent<BaseStats>();
            Animator = GetComponentInChildren<Animator_Base>();
        }

        public virtual IEnumerator TakeDamageCoroutine(float damage)
        {
            ResetState(false);
            if (BaseStats == null)
                BaseStats = GetComponent<BaseStats>();
            BaseStats.Health -= damage;
            if (BaseStats.Health <= 0)
            {
                BaseStats.Health = 0;
                Debug.Log("Monster is dead");
            }

            Debug.Log("Monster is taking damage");
            yield return new WaitForSeconds(PlayHurtAnimation());
            ResetState(true);
        }

        protected abstract float PlayHurtAnimation();

        private void ResetState(bool boolen)
        {
            allowExcuteAnotherAttack = boolen;
            allowCounter = boolen;
        }

        public void TakeDamage(float damage)
        {
            StartCoroutine(TakeDamageCoroutine(damage));
            Debug.Log($"Entity {gameObject.name} is taking damage");
        }
    }
}