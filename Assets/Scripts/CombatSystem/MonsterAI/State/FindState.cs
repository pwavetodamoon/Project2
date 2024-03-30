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
            Debug.Log("Find");
        }
    }
}