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

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
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
}