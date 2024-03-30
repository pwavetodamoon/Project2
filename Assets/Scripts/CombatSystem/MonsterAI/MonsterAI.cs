using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MonsterAI : Context
{
    public MoveState MoveState;
    public AttackState AttackState;
    public FindState FindState;
    public GameObject target;
    [Button]
    public override void ChangeState(StateEnum nextState)
    {
        switch (nextState)
        {
            case StateEnum.Move:
                currentState = MoveState; break;
            case StateEnum.Attack:
                currentState = AttackState; break;
            case StateEnum.Find:
                currentState = FindState; break;
        }
    }
    private void Update()
    {
        if (currentState != null)
            currentState.DoThis();
    }
}
public interface IState
{
    void DoThis();
}
public enum StateEnum
{
    None,
    Attack,
    Move,
    Find
}
public abstract class Context : MonoBehaviour
{
    public BaseIState currentState;
    public abstract void ChangeState(StateEnum nextState);
}
public abstract class BaseIState : MonoBehaviour, IState
{
    public MonsterAI context;
    private void Awake()
    {
        context = GetComponentInParent<MonsterAI>();
    }
    public abstract void DoThis();
}
