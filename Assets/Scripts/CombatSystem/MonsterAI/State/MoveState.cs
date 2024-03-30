using UnityEngine;

public class MoveState : BaseIState
{
    public float speed = 1;
    public float range = 1;
    public override void DoThis()
    {
        if (context.target != null && !IsOnTarget())
        {
            context.transform.position = Vector2.MoveTowards(context.transform.position, context.target.transform.position, speed * Time.deltaTime);
        }
        else if (context.target != null && IsOnTarget())
        {
            context.ChangeState(StateEnum.Attack);
        }
        else if (context.target == null)
        {
            context.ChangeState(StateEnum.Find);
        }
    }

    private bool IsOnTarget()
    {
        return Vector2.Distance(context.transform.position, context.target.transform.position) > range;
    }
}
