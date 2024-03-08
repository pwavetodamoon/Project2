using Model.Monsters;
using Sirenix.OdinInspector;

namespace Model.Hero
{
    public class Human_Animator : Animator_Base
    {
        public enum AnimationType
        {
            Idle,
            Walk,
            Slash,
            Hurt
        }

        private AnimationType currentType = AnimationType.Walk;

        public static AnimationType Idle_State
        {
            get => AnimationType.Idle;
            private set { }
        }

        public static AnimationType Walk_State
        {
            get => AnimationType.Walk;
            private set { }
        }

        public static AnimationType Slash_State
        {
            get => AnimationType.Slash;
            private set { }
        }

        public static AnimationType Hurt_State
        {
            get => AnimationType.Hurt;
            private set { }
        }

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

                case AnimationType.Slash:
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
            //Debug.Log("Change to idle");
            ChangeAnimation(currentType);
        }

        public override void SetDefaultAnimation<T>(T type)
        {
            currentType = (AnimationType)(object)type;
        }
    }
}