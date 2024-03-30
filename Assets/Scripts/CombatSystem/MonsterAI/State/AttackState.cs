using System.Collections;
using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Systems;
using UnityEngine;

public class AttackState : BaseIState
{
    public float timer;
    public float timerToAttack = 3;
    public EntityAttackControl entityAttackControl;
    public override void DoThis()
    {
        if (context.target != null)
        {
            Counter();
        }
        else
        {
            context.ChangeState(StateEnum.Find);
        }
    }

    private void Counter()
    {
        timer += Time.deltaTime;
        if (timer >= timerToAttack)
        {
            Debug.Log("Attack");
            Attack();
        }
    }
    private void Attack()
    {

    }
    private void ResetCounter()
    {

    }
}
