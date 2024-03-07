using System;
using Model.Monsters;
using UnityEngine;

namespace NewCombat.Entity.Utilities
{
    public class AnimationManager : MonoBehaviour
    {
        private Animator _animator;
        private Animator_Base animator_Base;
        private bool valueNotNull;

        private void Awake()
        {
            animator_Base = GetComponentInChildren<Animator_Base>();
            _animator = GetComponentInChildren<Animator>();
            if (animator_Base != null) valueNotNull = true;
        }

        public void PlayAnimation(Enum AnimationEnum)
        {
            if (!valueNotNull) return;
            animator_Base.ChangeAnimation(AnimationEnum);
        }

        public float GetAnimationLength(Enum AnimationEnum)
        {
            if (!valueNotNull) return 0;
            return animator_Base.GetAnimationLength(AnimationEnum);
        }

        public void DisableAnimator()
        {
            _animator.enabled = false;
        }

        public void EnableAnimator()
        {
            _animator.enabled = true;
        }
    }
}