using Characters;
using DG.Tweening;
using NewCombat.Characters;
using NewCombat.Helper;
using NewCombat.HeroAttack;
using NewCombat.ManagerInEntity;
using NewCombat.Slots;
using System.Collections;
using UnityEngine;

namespace NewCombat
{
    public class HeroNearAttack : BaseHeroAttack
    {
        protected HeroCharacter Hero;
        protected override IEnumerator StartBehavior()
        {
            yield return base.StartBehavior();
            var attackerTransform = Enemy.GetAttackerTransform();
            
            yield return MoveModelToPosition(attackerTransform.position);
            
            PlayAnimation(Human_Animator.Slash_State);
            var time = GetAnimationLength(Human_Animator.Slash_State);
            
            yield return new WaitForSeconds(time);
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

        private MonsterCharacter GetMonsterEntity(GameObject go)
        {
            return go.GetComponent<MonsterCharacter>();
        }


        private Vector3 GetSlotPosition()
        {
            return entityCharacter.transform.position;
        }

        public override void GetReference(EntityCharacter newEntityCharacter, AnimationManager _animationManager,
            AttackManager attackManager, Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, _animationManager, attackManager, attackTransform);
            Hero = (HeroCharacter)newEntityCharacter;
        }
    }
}