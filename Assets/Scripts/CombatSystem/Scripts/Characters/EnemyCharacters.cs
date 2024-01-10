using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class EnemyCharacters : CharactersBase
{
    private EnemyMoving moving;


    public bool Test = false;
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
    public void StartMoving()
    {
        enemy = CombatManager.GetEnemyPosition?.Invoke(enemyIndex);
        if (enemy == null)
            return;
        var newPos = enemy.SlotPosition.position;
        Debug.Log(newPos);
        transform.DOMove(newPos, 1).OnComplete(() =>
        {
            allowAction = true;
        });
    }
    protected override void InitCommandList()
    {
        float timeAttack = animator.GetAnimationLength(Monster_Animator.Attack_State);
        //AttackCommands.Add(new ActionCommand(MoveToEnemyCoroutine(Vector2.zero), null, 0));
        AttackCommands.Add(new ActionCommand(null, AttackEnemy, timeAttack + .1f));
        AttackCommands.Add(new ActionCommand(null, ResetState, 0));
        AttackCommands.Add(new ActionCommand(null, CheckEnemy, 0));
    }
    protected override void AttackEnemy()
    {
        base.AttackEnemy();
        animator.ChangeAnimation(Monster_Animator.Attack_State);
    }
    public override void Attack()
    {
        Debug.Log("Attack");
        attackSequence.AddListCommands(AttackCommands);
    }
    protected override void ResetState()
    {
        base.ResetState();
        animator.ChangeAnimation(Human_Animator.Walk_State);
    }
}