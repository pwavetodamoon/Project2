using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Monster_Animator : Animator_Base
{

    public enum AnimationType
    {
        Walk,
        Hurt,
        Attack
    }



    public void PlayAnimation(AnimationType type)
    {
        string animationName = GetAnimationName(type);
        animator.Play(animationName);
    }

    private string GetAnimationName(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.Walk:
                return "walk_1";

            case AnimationType.Hurt:
                return "hurt_1";

            case AnimationType.Attack:
                return "attack_1";

            default:
                return "";
        }
    }
    public AnimationType GetAnimationType(int indexState)
    {
        var CurrentState = AnimationType.Walk;
        switch (indexState)
        {
            case 0:
                CurrentState = AnimationType.Walk;
                break;
            case 1:
                CurrentState = AnimationType.Attack;
                break;
            case 2:
                CurrentState = AnimationType.Hurt;
                break;
            default:
                break;
        }
        return CurrentState;
    }
    /// <summary>
    /// 0 is walk, 1 is attack, 2 is hurt   
    /// </summary>
    /// <param name="indexState"></param>
    public override void ChangeState(int indexState)
    {
        var type = GetAnimationType(indexState);
        PlayAnimation(type);
    }
}
public abstract class Animator_Base: MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public abstract void ChangeState(int indexState);
}