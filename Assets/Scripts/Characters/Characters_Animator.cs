using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class Characters_Animator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public enum AnimationType
    {
        Idle,
        Walk,
        Slash,
        Hurt,
    }
    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
    void PlayAnimation(AnimationType type)
    {
        if(animator != null)
        {
            Debug.LogError("Animator is null");
        }
        var animationName = GetAnimationName(type);
        animator.Play(animationName);

    }
    string GetAnimationName(AnimationType type)
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
