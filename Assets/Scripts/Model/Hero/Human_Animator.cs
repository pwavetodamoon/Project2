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
        Shooting,
        Spell,
        Dying
    }
    public class Human_Animator : Animator_Base
    {
        /// <summary>
        ///     Idle = 0, Walk = 1, Slash = 2, Hurt = 3
        /// </summary>
        /// <param name="indexState"></param>
        protected override string GetAnimationNameByType<T>(T type)
        {
            switch (type)
            {
                case AnimationType.Idle:
                    return "Idle";

                case AnimationType.Walk:
                    return "Walking";

                case AnimationType.Attack:
                    return "Slashing";

                case AnimationType.Hurt:
                    return "Hurt";
                case AnimationType.Dying:
                    return "Dying";
                case AnimationType.Shooting:
                    return "Shooting";
                case AnimationType.Spell:
                    return "Spell";

                default:
                    return "Ilde";
            }
        }

        [Button]
        public void PlayAnimOnInspector(AnimationType animationType)
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