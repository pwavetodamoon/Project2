using System.Collections;
using CombatSystem.Attack.Abstracts;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using DG.Tweening;
using Model.Hero;
using SlotHero;
using UnityEngine;

namespace CombatSystem.Attack.Near
{
    public class HeroNearAttack : BaseHeroAttack
    {
        protected HeroCharacter Hero;
        private WaitForSeconds waitForEndAnim;

        protected override IEnumerator StartBehavior()
        {
            yield return base.StartBehavior();
            yield return MoveModelToPosition(Enemy.GetAttackerTransform().position);
            PlayAnimation(AnimationType.Attack);
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

        public override void GetReference(EntityCharacter newEntityCharacter, Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, attackTransform);

            Hero = (HeroCharacter)newEntityCharacter;
            waitForEndAnim = new WaitForSeconds(GetAnimationLength(AnimationType.Attack));
        }
    }
}