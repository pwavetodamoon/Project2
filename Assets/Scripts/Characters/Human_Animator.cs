using UnityEngine;

public class Human_Animator : Animator_Base
{
    public AnimationType CurrentState;
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
    public override void ChangeState(int indexState)
    {
        CurrentState = GetAnimationType(indexState);
        PlayAnimation(CurrentState);
    }
    private AnimationType GetAnimationType(int indexState)
    {
        var CurrentState = AnimationType.Idle;
        switch (indexState)
        {
            case 0:
                CurrentState = AnimationType.Idle;
                break;
            case 1:
                CurrentState = AnimationType.Walk;
                break;
            case 2:
                CurrentState = AnimationType.Slash;
                break;
            case 3:
                CurrentState = AnimationType.Hurt;
                break;
            default:
                break;
        }
        return CurrentState;
    }
    private void PlayAnimation(AnimationType type)
    {
        var animationName = GetAnimationName(type);
        animator.Play(animationName);
    }
    public float GetCurrentAnimationLength()
    {
        return GetAnimationLength(CurrentState);
    }
    private float GetAnimationLength(AnimationType type)
    {
        var name = GetAnimationName(type);
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip.length;
            }
        }
        Debug.LogError("The animation not exits");
        return -1;
    }
    private string GetAnimationName(AnimationType type)
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
                return "";
        }
    }

}
