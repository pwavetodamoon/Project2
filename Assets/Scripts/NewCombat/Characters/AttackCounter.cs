using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AttackCounter
{
    public float timeCounter;
    public float maxTime = 0.5f;
    public int attackCount;
    public int maxAttackCount = 3;
    public Action CallbackEvent;
    public void CheckTimerCounter(HeroNormalAttack heroNormalAttack, float time)
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
