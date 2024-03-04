using Characters.Monsters;
using System;
using UnityEngine;

namespace NewCombat.ManagerInEntity
{
    public class AnimationManager : MonoBehaviour
    {
        private Animator_Base animator_Base;
        private bool valueNotNull = false;

        private void Awake()
        {
            animator_Base = GetComponentInChildren<Animator_Base>();
            if (animator_Base != null)
            {
                valueNotNull = true;
            }
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
    }
}