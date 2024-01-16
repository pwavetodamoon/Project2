using System;

namespace NewCombat.Characters
{
    public class AttackCounter
    {
        public float timeCounter;
        public float maxTime = 0.5f;
        public int attackCount;
        public int maxAttackCount = 3;
        public Action CallbackEvent;
        public void CheckTimerCounter(BaseHeroNormalAttack heroNormalAttack, float time)
        {
            if (timeCounter > 0 && heroNormalAttack.isActive == false)
                timeCounter -= time;
            else if (timeCounter <= 0 && heroNormalAttack.isActive == false)
            {
                timeCounter = maxTime;
                heroNormalAttack.ExecuteAttack();
            }

        }
    }
}
