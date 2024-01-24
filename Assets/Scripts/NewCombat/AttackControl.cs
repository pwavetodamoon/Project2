using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewCombat.HeroAttack;
using NewCombat.Characters;
using System;
using Sirenix.OdinInspector;
public struct AttackInfo
{
    public AttackInfo(BaseNormalAttack attack, AttackCounter counter)
    {
        this.attack = attack;
        this.counter = counter;
    }
    public BaseNormalAttack attack;
    public AttackCounter counter;
}
public class AttackControl : MonoBehaviour, ICoroutineRunner
{
    [ShowInInspector] public AttackInfo attackInfor;
    public Transform AttackTransform;
    public EntityCharacter entityCharacter;
    public float maxCounterTime = 3f;
    private void Update()
    {
        UpdateTimer();
    }
    private void UpdateTimer()
    {
        if (attackInfor.counter != null)
        {
            bool allowCounter = entityCharacter.IsAllowCounter();
            bool allowToExcuteAnotherAttack = entityCharacter.IsAllowAttack();
            bool isActive = attackInfor.attack.IsActive;
            attackInfor.counter.CheckTimerCounter(allowCounter, isActive, allowToExcuteAnotherAttack, Time.deltaTime);
        }
    }
    [Button]
    public void InitAttackControl(BaseNormalAttack attack)
    {
        attack.AttackTransform = AttackTransform;
        var counter = new AttackCounter(maxCounterTime);
        counter.CoroutineRunner = this;
        counter.AttackAction = attack.ExecuteAttack;

        attack.OnEndAttack += counter.ResetCounter;


        attackInfor = new AttackInfo(attack, counter);
    }
    private void ResetCounter()
    {
        attackInfor.counter.ResetCounter();
    }
}
public interface ICoroutineRunner
{
    Coroutine StartCoroutine(IEnumerator routine);
}
