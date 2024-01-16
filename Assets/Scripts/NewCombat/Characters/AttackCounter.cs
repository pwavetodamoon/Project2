using System;
using UnityEngine;

namespace NewCombat.Characters
{
    public class AttackCounter
    {
        public AttackCounter(){}
        public float timeCounter;
        public float maxTime = 0.5f;
        [HideInInspector]
        public Action AttackAction;
        public void CheckTimerCounter(bool state,float time)
        {
            if (timeCounter > 0 && state == false)
            {
                timeCounter -= time;
            }
            else if (timeCounter <= 0 && state == false)
            {
                // Excute attack method register                
                AttackAction?.Invoke();
            }
        }

        public void ResetCounter()
        {
            timeCounter = maxTime;
        }
    }
}
