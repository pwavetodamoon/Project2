using System.Collections;
using DG.Tweening;
using Model.Hero;
using NewCombat.Attack.Abstracts;
using NewCombat.Entity;
using NewCombat.Entity.Utilities;
using SlotHero;
using UnityEngine;

namespace NewCombat.Attack.Near
{
    public class HeroNearAttack : BaseHeroAttack
    {
        protected HeroCharacter Hero;
        private WaitForSeconds waitForEndAnim;

        protected override IEnumerator StartBehavior()
        {
            yield return base.StartBehavior();
            yield return MoveModelToPosition(Enemy.GetAttackerTransform().position);
            PlayAnimation(Human_Animator.Slash_State);
            yield return waitForEndAnim;
            CauseDamage();
            yield return MoveModelToPosition(GetSlotPosition());
        }

        private IEnumerator MoveModelToPosition(Vector3 position)
        {
            var model = Hero.GetModelTransform();
            yield return model.DOMove(position, EntityStats.AttackMoveDuration())
                .SetEase(Ease.OutCubic)
                .WaitForCompletion();
        }

        private Vector3 GetSlotPosition()
        {
            return SlotManager.Instance.GetStandTransform(Hero.InGameSlotIndex).position;
        }

        public override void GetReference(EntityCharacter newEntityCharacter, AnimationManager _animationManager,
            AttackManager attackManager, Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, _animationManager, attackManager, attackTransform);

            Hero = (HeroCharacter)newEntityCharacter;
            waitForEndAnim = new WaitForSeconds(GetAnimationLength(Human_Animator.Slash_State));
        }
    }
}