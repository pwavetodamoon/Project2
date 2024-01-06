using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

public abstract class Animator_Base : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public virtual void ChangeState<T>(T enumType) where T : Enum
    {
        string animationName = GetAnimationNameByType(enumType);
        animator.Play(animationName);
        // FIXME: This is not a good way to check animation is loop or not
        // code check current animation is loop or not
        //Debug.Log("Ae: "+animationName);
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
            CallBackAnimation();
        }
    }
    /// <summary>
    /// Call when a animation is done with not have loop
    /// </summary>
    protected abstract void CallBackAnimation();
    //protected abstract T _GetAnimationType<T>(int index) where T : Enum;
    protected abstract string GetAnimationNameByType<T>(T type) where T : Enum;

}