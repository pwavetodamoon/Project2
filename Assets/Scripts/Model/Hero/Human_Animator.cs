using Model.Monsters;
using Sirenix.OdinInspector;

namespace Model.Hero
{
    public enum AnimationType
    {
        Idle,
        Walk,
        Hurt,
        Attack,
        Dying
    }
    public class Human_Animator : Animator_Base
    {
        /// <summary>
        ///     Idle = 0, Walk = 1, Slash = 2, Hurt = 3
        /// </summary>
        /// <param name="indexState"></param>
        public override void ChangeAnimation<T>(T Type)
        {
            base.ChangeAnimation(Type);
        }

        protected override string GetAnimationNameByType<T>(T type)
        {
            switch (type)
            {
                case AnimationType.Idle:
                    return "Idle_1";

                case AnimationType.Walk:
                    return "walking_1";

                case AnimationType.Attack:
                    return "slashing_1";

                case AnimationType.Hurt:
                    return "hurt_1";

                default:
                    return "Idle_1";
            }
        }

        [Button]
        public void Test(AnimationType animationType)
        {
            ChangeAnimation(animationType);
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