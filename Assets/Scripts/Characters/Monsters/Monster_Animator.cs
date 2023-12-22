using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Monster_Animator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public enum AnimationType
    {
        Walk,
        Hurt,
        Attack
    }

    public void Walk()
    {
        PlayAnimation(AnimationType.Walk);
    }

    public void Hurt()
    {
        PlayAnimation(AnimationType.Hurt);
    }

    public void Attack()
    {
        PlayAnimation(AnimationType.Attack);
    }

    [DisableInEditorMode]
    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
    private void PlayAnimation(AnimationType type)
    {
        var animationName = GetAnimationName(type);
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
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
}