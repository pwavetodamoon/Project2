using Sirenix.OdinInspector;

namespace Model.Monsters
{
    public class Monster_Animator : Animator_Base
    {
        public enum AnimationType
        {
            Idle,
            Walk,
            Hurt,
            Attack,
            Dying
        }

        public AnimationType currentAnimation = Walk_State;

        public static AnimationType Walk_State
        {
            get => AnimationType.Walk;
            private set { }
        }

        public static AnimationType Hurt_State
        {
            get => AnimationType.Hurt;
            private set { }
        }

        public static AnimationType Attack_State
        {
            get => AnimationType.Attack;
            private set { }
        }

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
            ChangeAnimation(Walk_State);
        }

        public override void SetDefaultAnimation<T>(T type)
        {
            currentAnimation = (AnimationType)(object)type;
        }
    }
}