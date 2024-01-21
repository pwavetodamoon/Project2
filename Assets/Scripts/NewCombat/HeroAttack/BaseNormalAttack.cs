using System.Collections;
using Characters.Monsters;
using NewCombat.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.HeroAttack
{
    public abstract class BaseNormalAttack : MonoBehaviour
    {
        protected Animator_Base animator;


        public bool IsActive = false;

        [Header("Counter Setting/ BaseClass")] 
        
        public float maxCounterTime = 3f;

        [ProgressBar(0, "maxCounterTime", Height = 30)] 
        [SerializeField] protected float timerCounterInspector = 0;

        private AttackCounter attackCounter;
        private EntityCharacter entityCharacter;
        protected CombatCollider CombatCollider;

        protected virtual void CounterForAttack()
        {
            if(maxCounterTime >= 0)
            {
                attackCounter.UpdateMaxCounterTime(maxCounterTime);
            }
            attackCounter.CheckTimerCounter(entityCharacter.allowCounter,IsActive, entityCharacter.allowExcuteAnotherAttack, Time.deltaTime);
            timerCounterInspector = attackCounter.timeCounter;
        }

        protected virtual void Awake()
        {
            entityCharacter = GetComponent<EntityCharacter>();
            attackCounter = new AttackCounter(maxCounterTime);
            animator = GetComponentInChildren<Animator_Base>();
            CombatCollider = new CombatCollider(entityCharacter);

        }
        private void OnEnable()
        {
            attackCounter.AttackAction += ExecuteAttack;
        }
        private void OnDisable()
        {
            attackCounter.AttackAction -= ExecuteAttack;
        }
        private void Update()
        {
            CounterForAttack();
        }
        protected virtual IEnumerator StartBehavior()
        {
            // Method use for create behavior of attack
            // Make sure to call ResetStateAndCounter() at the end of the method
            // If enemy is null, break the method and reset IsActive to false
            yield return null;
        }
        public void ExecuteAttack()
        {
            // This method is called when the counter is finished
            if (IsActive) return;
            IsActive = true;
            //StopAllCoroutines();
            StartCoroutine(StartBehavior());
        }

        protected virtual void ResetStateAndCounter()
        {
            // Call in StartBehavior and at final the attack to reset state and counter
            IsActive = false;
            entityCharacter.allowExcuteAnotherAttack = true;
            attackCounter.ResetCounter();
        }

        protected virtual void CauseDamage(string Tag)
        {
            
        }
    }
}