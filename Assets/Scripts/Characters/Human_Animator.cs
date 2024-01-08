using Sirenix.OdinInspector;
using UnityEngine;

public class Human_Animator : Animator_Base
{
    public static AnimationType Idle_State { get => AnimationType.Idle; private set { } }
    public static AnimationType Walk_State { get => AnimationType.Walk; private set { } }
    public static AnimationType Slash_State { get => AnimationType.Slash; private set { } }
    public static AnimationType Hurt_State { get => AnimationType.Hurt; private set { } }

    public enum AnimationType
    {
        Idle,
        Walk,
        Slash,
        Hurt,
    }

    /// <summary>
    /// Idle = 0, Walk = 1, Slash = 2, Hurt = 3
    /// </summary>
    /// <param name="indexState"></param>
    public override void ChangeAnimation<T>(T Type)
    {
        base.ChangeAnimation(Type);
    }
    [Button]
    void Test(AnimationType type)
    {
        ChangeAnimation(type);
    }
    [Button]
    void Test2()
    {
        bool test = animator.GetCurrentAnimatorStateInfo(0).loop;
        if(test)
        {
            Debug.Log("Animation is loop");
        }
        else
        {
            Debug.Log("Animation is not loop");
        }
    }
    protected override string GetAnimationNameByType<T>(T type)
    {
        switch (type)
        {
            case AnimationType.Idle:
                return "Idle_1";

            case AnimationType.Walk:
                return "walking_1";

            case AnimationType.Slash:
                return "slashing_1";

            case AnimationType.Hurt:
                return "hurt_1";

            default:
                return "Idle_1";
        }
    }

    protected override void CallBackAnimation()
    {
        //Debug.Log("Change to idle");
        ChangeAnimation(Idle_State);
    }
}
