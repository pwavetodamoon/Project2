using System;
using System.Collections;
using Characters.Monsters;
using NewCombat.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat
{
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

        [ProgressBar(0, "maxCounterTime", Height = 30)] 
        [SerializeField] private float timerCounter = 0;
        // Features
        protected AttackCounter attackCounter;

        private void Update()
        {
            if(maxCounterTime >= 0)
            {
                attackCounter.UpdateMaxCounterTime(maxCounterTime);
            }
            attackCounter.CheckTimerCounter(IsActive, Hero.allowExcuteAnotherAttack, Time.deltaTime);
            timerCounter = attackCounter.timeCounter;
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
            yield return null;
        }

        protected virtual void OnDrawGizmos(){}
        [Button]
        protected virtual void CheckCollider(){}


        // Execute the attack, this is interface method
        public void ExecuteAttack()
        {
            if (IsActive) return;
            IsActive = true;
            StartCoroutine(StartBehavior());
        }

        /// <summary>
        /// Use to reset states of attacker after attack:
        /// </summary>
        protected virtual void ResetStateAndCounter()
        {
            IsActive = false;
            Hero.allowExcuteAnotherAttack = true;
            attackCounter.ResetCounter();
        }
    }
}