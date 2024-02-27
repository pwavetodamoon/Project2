using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace NewCombat.Characters
{
    [Serializable]
    public class AttackCounter
    {
        [ProgressBar(0, "maxCounterTime", Height = 30)]
        [SerializeField]
        protected float timerCounterInspector;

        public float timeCounter;
        public float maxCounterTime = 3f;
        private bool AllowToExecuteAnotherAttack;

        private Func<IEnumerator> AttackAction;
        private bool AttackIsActive;
        private bool CanCounter;
        private float maxTime = 3f;

        public AttackCounter(float maxTimeCounter)
        {
            maxTime = maxTimeCounter;
            timeCounter = maxTimeCounter;
        }

        private ICoroutineRunner CoroutineRunner { get; set; }

        public void SetCoroutineRunner(ICoroutineRunner theRunner)
        {
            CoroutineRunner = theRunner;
        }

        public void SetAttackCallBack(Func<IEnumerator> callback)
        {
            AttackAction = callback;
        }

        public void UpdateNewControlState(bool CanCounter, bool attackIsActive, bool allowToExcuteAnotherAttack)
        {
            this.CanCounter = CanCounter;
            AttackIsActive = attackIsActive;
            AllowToExecuteAnotherAttack = allowToExcuteAnotherAttack;
        }

        public void CheckTimerCounter(float time)
        {
            if (AllowCounter())
            {
                timeCounter -= time;
                timerCounterInspector = timeCounter;
            }
            else if (AllowAttack())
            {
                //AttackAction?.Invoke();
                CoroutineRunner.StartCoroutine(AttackAction());
            }
        }

        private bool AllowCounter()
        {
            return CanCounter && timeCounter > 0 && AttackIsActive == false;
        }

        private bool AllowAttack()
        {
            return AllowToExecuteAnotherAttack && timeCounter <= 0 && AttackIsActive == false;
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