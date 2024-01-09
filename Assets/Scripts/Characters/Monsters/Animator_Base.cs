using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Animator_Base : MonoBehaviour, IChangeAnimation, IGetAnimationLength
{
    [SerializeField] protected Animator animator;
    protected Dictionary<string,float> animationLengths;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        // add a array of animation to dictionary with 2 value is name and length(string,float)
        animationLengths = animator.runtimeAnimatorController.animationClips.ToDictionary(clip => clip.name, clip => clip.length);
    }
    public virtual void ChangeAnimation<T>(T type1) where T : Enum
    {
        string animationName = GetAnimationNameByType(type1);
        animator.Play(animationName);
        StartCoroutine(WaitAnimation());
    }
    IEnumerator WaitAnimation()
    {
        yield return new WaitForEndOfFrame();
        var clipInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (clipInfo.loop == false)
        {
            //Debug.Log("Animation is not loop");
            animHaveLoopIsRun = true;
            timeAnimated = clipInfo.length * clipInfo.speed;
        }
    }

    [SerializeField] protected bool animHaveLoopIsRun = false;
    [SerializeField] protected float timeAnimated = 0;
    protected virtual void Update()
    {
        if (!animHaveLoopIsRun)
        {
            return;
        }
        timeAnimated -= Time.deltaTime;
        if (timeAnimated <= 0)
        {
            animHaveLoopIsRun = false;
            ChangeToDefaultAnimationState();
        }
    }
    /// <summary>
    /// Call when a animation is done with not have loop
    /// </summary>
    protected abstract void ChangeToDefaultAnimationState();
    //protected abstract T _GetAnimationType<T>(int index) where T : Enum;
    protected abstract string GetAnimationNameByType<T>(T type) where T : Enum;
    public abstract void SetDefaultAnimation<T>(T type) where T : Enum;
    public float GetAnimationLength<T>(T type) where T : Enum
    {
        var animName = GetAnimationNameByType(type);
        return animationLengths.TryGetValue(animName, out var length) ? length : 0;
    }
}
public interface IChangeAnimation
{
    void ChangeAnimation<T>(T type) where T : Enum;
}
public interface IGetAnimationLength
{
    float GetAnimationLength<T>(T type) where T : Enum;
}
