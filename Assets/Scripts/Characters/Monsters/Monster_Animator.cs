using Sirenix.OdinInspector;

namespace Characters.Monsters
{
    public class Monster_Animator : Animator_Base
    {
        public static AnimationType Walk_State { get => AnimationType.Walk; private set { } }
        public static AnimationType Hurt_State { get => AnimationType.Hurt; private set { } }
        public static AnimationType Attack_State { get => AnimationType.Attack; private set { } }
        public AnimationType currentAnimation = Walk_State;

        public enum AnimationType
        {
            Walk,
            Hurt,
            Attack
        }

        /// <summary>
        /// 0 is walk, 1 is attack, 2 is hurt   
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
                case AnimationType.Walk:
                    return "walk_1";

                case AnimationType.Hurt:
                    return "hurt_1";

                case AnimationType.Attack:
                    return "attack_1";

                default:
                    return "walk_1";
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
