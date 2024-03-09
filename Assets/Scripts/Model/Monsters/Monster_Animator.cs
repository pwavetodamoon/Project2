using Model.Hero;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Model.Monsters
{
    public class Monster_Animator : Animator_Base
    {
        /// <summary>
        ///     0 is walk, 1 is attack, 2 is hurt
        /// </summary>
        /// <param name="indexState"></param>
        public override void ChangeAnimation<T>(T type)
        {
            base.ChangeAnimation(type);
        }

        protected override string GetAnimationNameByType<T>(T type)
        {
            switch (type)
            {
                case AnimationType.Idle:
                    return "Idle";
                case AnimationType.Walk:
                    return "Walking";
                case AnimationType.Hurt:
                    return "Hurt";
                case AnimationType.Attack:
                    return "Attack";
                case AnimationType.Dying:
                    return "Dying";
                default:
                    return "Idle";
            }
        }

        [Button]
        public void Test(AnimationType type)
        {
            ChangeAnimation(type);
        }

        protected override void ChangeToDefaultAnimationState()
        {
            ChangeAnimation(defaultAnimationPlayBack);
        }

        public override void SetDefaultAnimation<T>(T type)
        {
            defaultAnimationPlayBack = (AnimationType)(object)type;
        }
    }
}