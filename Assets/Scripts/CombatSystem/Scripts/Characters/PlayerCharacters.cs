using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class PlayerCharacters : CharactersBase
{
    //public static PlayerCharacters Instance;
    public bool Test = false;
    public float timeAttack = 0;
    protected override void Start()
    {
        base.Start();
        timeAttack = animator.GetAnimationLength(Human_Animator.Slash_State);
        timeCounter = data.timeCoolDown + .1f;
    }
    protected override void InitCommandList()
    {
        ICommand moveCommand = new ActionCommand(MoveToEnemyCoroutine(Vector2.zero));
        ICommand attackCommand = new ActionCommand(null, AttackEnemy, timeAttack);
        ICommand moveCommand2 = new ActionCommand(MoveToEnemyCoroutine(Vector2.zero));
        ICommand attackCommand2 = new ActionCommand(null, ResetState, 0);

        AttackCommands.Add(moveCommand);
        AttackCommands.Add(attackCommand);
        AttackCommands.Add(moveCommand2);
        AttackCommands.Add(attackCommand2);
    }
    private void Update()
    {
        AttackMechanism();
        TimerCounterMechanism();
    }
    private void TimerCounterMechanism()
    {
        if (Test == true || attacking)
        {
            return;
        }
        if (timeCounter >= 0)
        {
            timeCounter -= Time.deltaTime;
        }
        else
        {
            timeCounter = 0;
        }
    }
    public override void Attack()
    {
        var enemyPos = (Vector2)enemy.transform.position + new Vector2(-2, 0);
        //timeAttack = animator.GetAnimationLength(Human_Animator.Slash_State);
        var originalPos = transform.position;
        //Debug.Log("Time attack: " + timeAttack);
        AttackCommands[0] = new ActionCommand(MoveToEnemyCoroutine(enemyPos));
        AttackCommands[1] = new ActionCommand(null, AttackEnemy, timeAttack);
        AttackCommands[2] = new ActionCommand(MoveToEnemyCoroutine(originalPos));
        AttackCommands[3] = new ActionCommand(null, ResetState, 0);

        attackSequence.AddListCommands(AttackCommands);
        Debug.Log("Start attact");
    }

    protected override void AttackEnemy()
    {
        Debug.Log("Attack to enemy");
        base.AttackEnemy();
        animator.ChangeAnimation(Human_Animator.Slash_State);
    }
    protected override void ResetState()
    {
        Debug.Log("Reset state");
        base.ResetState();
        animator.ChangeAnimation(Human_Animator.Idle_State);
    }
}
