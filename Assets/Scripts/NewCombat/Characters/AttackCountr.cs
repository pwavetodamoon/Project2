using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCountr
{
    public float timeCounter;
    public float maxTime = 0.5f;
    public int attackCount;
    public int maxAttackCount = 3;
    private void Update()
    {
        if(timeCounter > 0)
            timeCounter -= Time.deltaTime;
        else
        {
            timeCounter = 0;
        }
    }
    public void CountingAttack()
    {
        attackCount++;
    }
    public void ResetCounter()
    {
        timeCounter = maxTime;
        attackCount = 0;
    }
}
