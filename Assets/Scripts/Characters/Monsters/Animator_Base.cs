using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Characters.Monsters
{
    public abstract class Animator_Base : MonoBehaviour, IChangeAnimation, IGetAnimationLength
    {
        [SerializeField] protected Animator animator;
        [ShowInInspector] protected Dictionary<string, float> animationLengths;
        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            // add a array of animation to dictionary with 2 value is name and length(string,float)
            animationLengths = animator.runtimeAnimatorController.animationClips.ToDictionary(clip => clip.name, clip => clip.length);
        }
        public virtual void ChangeAnimation<T>(T type1) where T : Enum
        {
            //StopAllCoroutines();
            //isPlayNewAnimation = true;
            //animator.StopPlayback();
            string animationName = GetAnimationNameByType(type1);
            //Debug.Log("Play Animation: "+animationName);
            animator.Play(animationName,0,0);
            StartCoroutine(WaitAnimation());
        }
        IEnumerator WaitAnimation()
        {
            yield return new WaitForEndOfFrame();
            //Debug.Log("Go in WaitAnimation");
            var clipInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (clipInfo.loop == false)
            {
                //Debug.Log("Animation not have loop");
                animationNotHaveLoopIsRun = true;
                timeAnimated = clipInfo.length * clipInfo.speed;
            }
            else
            {
                //Debug.Log("Animation have loop");
            }

            isPlayNewAnimation = false;
        }

        [SerializeField] protected bool animationNotHaveLoopIsRun = false;
        [SerializeField] protected float timeAnimated = 0;
        private bool isPlayNewAnimation;
        protected virtual void Update() 
        {
            if (!animationNotHaveLoopIsRun)
            {
                return;
            }
            timeAnimated -= Time.deltaTime;
            if (timeAnimated <= 0)
            {
                animationNotHaveLoopIsRun = false;
                ChangeToDefaultAnimationState();
            }
        }
        /// <summary>
        /// Call when a animation is done with not have loop
        /// </summary>
        protected abstract void ChangeToDefaultAnimationState();
        //protected abstract T _GetAnimationType<T>(int enemyIndex) where T : Enum;
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
}