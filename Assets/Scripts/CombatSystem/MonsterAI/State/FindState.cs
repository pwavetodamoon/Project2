using CombatSystem;
using Helper;
using UnityEngine;

public class FindState : BaseIState
{
    public override void DoThis()
    {
        if (context.target != null)
        {
            context.ChangeState(StateEnum.Move);
        }
        else
        {
            context.target = CombatEntitiesManager.Instance.GetEntityTransformByTag(transform, GameTag.Hero).gameObject;
            Debug.Log("Find: "+context.target.name);
        }
    }
}