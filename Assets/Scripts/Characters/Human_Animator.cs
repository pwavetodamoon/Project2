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

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayAnimation(AnimationType type)
    {
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
                return "";
        }
    }
}
