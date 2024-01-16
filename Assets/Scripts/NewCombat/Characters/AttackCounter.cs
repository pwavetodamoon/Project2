using System;
using UnityEngine;

namespace NewCombat.Characters
{
    public class AttackCounter
    {
        public AttackCounter(float maxTimeCounter = 0.5f)
        {
            maxTime = maxTimeCounter;
            timeCounter = maxTimeCounter;
        }
        public float timeCounter;
        private float maxTime = 0.5f;
        public Action AttackAction;
        public void CheckTimerCounter(bool attackIsActive, bool allowToExcuteAnotherAttack,float time)
        {
            bool allowAttack = false;
            bool allowCounter = false;

            // The counter can count even when the allowToExcuteAnotherAttack is false
            allowCounter = timeCounter > 0 && attackIsActive == false;

            // The attack can excute when:
            // 'allowToExcuteAnotherAttack' is true
            // timeCounter is 0
            // current attackIsActive is false
            allowAttack = allowToExcuteAnotherAttack == true && timeCounter <= 0 && attackIsActive == false;

            if (allowCounter)
            {
                timeCounter -= time;
            }
            else if (allowAttack)
            {
                AttackAction?.Invoke();
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
