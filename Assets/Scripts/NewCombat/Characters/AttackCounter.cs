using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
namespace NewCombat.Characters
{
    [Serializable]
    public class AttackCounter 
    {
        public float maxCounterTime = 3f;

        [ProgressBar(0, "maxCounterTime", Height = 30)]
        [SerializeField] protected float timerCounterInspector = 0;
        public AttackCounter(float maxTimeCounter = 0.5f)
        {
            maxTime = maxTimeCounter;
            timeCounter = maxTimeCounter;
        }
        public float timeCounter;
        private float maxTime = 3f;
        
        
        public Func<IEnumerator> AttackAction;
        public ICoroutineRunner CoroutineRunner { get; set; }


        public void CheckTimerCounter(bool CanCounter, bool attackIsActive, bool allowToExcuteAnotherAttack, float time)
        {
            bool allowAttack = false;
            bool allowCounter = false;
            // Time counter
            allowCounter = CanCounter == true && timeCounter > 0 && attackIsActive == false;
            // Attack
            allowAttack = allowToExcuteAnotherAttack == true && timeCounter <= 0 && attackIsActive == false;
            //allowAttack = allowToExcuteAnotherAttack == true && timeCounter <= 0;
            if (allowCounter)
            {
                timeCounter -= time;
                timerCounterInspector = timeCounter;
            }
            else if (allowAttack)
            {
                //AttackAction?.Invoke();
                CoroutineRunner.StartCoroutine(AttackAction());
            }
        }
        public void ResetCounter()
        {
            timeCounter = maxTime;
        }

        public void UpdateMaxCounterTime(float newMaxTimeCounter)
        {
            maxTime = newMaxTimeCounter;
        }
    }
}
