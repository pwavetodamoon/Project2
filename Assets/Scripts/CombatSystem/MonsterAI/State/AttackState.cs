using System.Collections;
using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Systems;
using CombatSystem.Entity;
using UnityEngine;

public class AttackState : BaseIState
{
    public float timer;
    public float timerToAttack = 3;
    public EntityAttackControl entityAttackControl;
    private void Start()
    {
        entityAttackControl.OnEndAttack += ResetCounter;
    }
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
            if(entityAttackControl == null)
            {
                Debug.LogWarning("Entity Attack Control is null");
                return;
            }
            entityAttackControl.Attack(context.target.GetComponent<EntityCharacter>());

        }
    }
    private void ResetCounter()
    {
        timer = 0;
    }
}
