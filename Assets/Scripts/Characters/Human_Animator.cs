using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_Animator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public enum AnimationType
    {
        Idle,
        Walk,
        Slash,
        Hurt,
    }

    public void Slashing()
    {
        PlayAnimation(AnimationType.Slash);
    }

    public void Idle()
    {
        PlayAnimation(AnimationType.Idle);
    }

    public void Walking()
    {
        PlayAnimation(AnimationType.Walk);
    }

    public void Hurt()
    {
        PlayAnimation(AnimationType.Hurt);
    }

    [DisableInEditorMode]
    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
    private void PlayAnimation(AnimationType type)
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        var animationName = GetAnimationName(type);
        animator.Play(animationName);
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
                break;
        }
        return null;
    }
}