using System;
using System.Collections;
using Characters.Monsters;
using NewCombat.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat
{
    // Explain class:
    // Properties:
    // - animator: use to change animation
    // - IsActive: use to check if this attack is actived
    // OnDisable, OnEnable :
    // - Use to subscribe and unsubscribe event in AttackCounter (Excute Attack)
    // Update:
    // - Update Max Counter time in AttackCounter class
    // - Check timer counter in AttackCounter class and excute event Excute Attack
    public abstract class BaseHeroNormalAttack : MonoBehaviour
    {

        [Header("References/BaseClass")] 
        protected Animator_Base animator;
        public Transform gizmosTransform;
        public HeroCharacter Hero;

        [Header("Vector2/BaseClass")] 
        public Vector2 gizmosPosition;
        public Vector2 size = Vector3.one;

        [Header("Value/BaseClass")] 
        public float Angle = 0;
        public float Speed = 2;
        public bool IsActive = false;

        [Header("Counter Setting/ BaseClass")] 
        
        public float maxCounterTime = 3f;
        // create a progress bar with a range from 0 to 100, 
        [ProgressBar(0, "maxCounterTime", Height = 30)] 
        [SerializeField] private float timerCounterInspector = 0;
        // Features
        protected AttackCounter attackCounter;

        private void Update()
        {
            if(maxCounterTime >= 0)
            {
                attackCounter.UpdateMaxCounterTime(maxCounterTime);
            }
            attackCounter.CheckTimerCounter(Hero.allowCounter,IsActive, Hero.allowExcuteAnotherAttack, Time.deltaTime);
            timerCounterInspector = attackCounter.timeCounter;
            // Debug.Log("TimeCounter: "+attackCounter.timeCounter);
        }

        private void Awake()
        {
            attackCounter = new AttackCounter(maxCounterTime);
            animator = GetComponentInChildren<Animator_Base>();
            Hero = GetComponent<HeroCharacter>();
        }

        private void OnEnable()
        {
            attackCounter.AttackAction += ExecuteAttack;
        }

        private void OnDisable()
        {
            attackCounter.AttackAction -= ExecuteAttack;
        }

        protected virtual IEnumerator StartBehavior()
        {
            // Method use for create behavior of attack
            // Make sure to call ResetStateAndCounter() at the end of the method
            // If enemy is null, break the method and reset IsActive to false
            yield return null;
        }

        protected virtual void OnDrawGizmos(){}
        [Button]
        protected virtual void CheckCollider(){}
        public void ExecuteAttack()
        {
            // This method is called when the counter is finished
            if (IsActive) return;
            IsActive = true;
            StartCoroutine(StartBehavior());
        }

        protected virtual void ResetStateAndCounter()
        {
            // Call in StartBehavior and at final the attack to reset state and counter
            IsActive = false;
            Hero.allowExcuteAnotherAttack = true;
            attackCounter.ResetCounter();
        }
    }
}