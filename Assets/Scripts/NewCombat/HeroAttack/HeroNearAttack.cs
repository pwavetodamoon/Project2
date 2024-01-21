using System.Collections;
using Characters;
using CombatSystem;
using NewCombat.Characters;
using NewCombat.MonsterAttack;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.HeroAttack
{
    public class HeroNearAttack : BaseNearAttack
    {
        public float Speed = 2;
        protected override IEnumerator StartBehavior()
        {
            var monster = CombatManager.Instance.GetMonster();
            if (monster == null)
            {
                IsActive = false;
                yield break;
            }
            monster.notMoving = true;

            // Go to enemy
            entityCharacter.allowExcuteAnotherAttack = false;

            yield return MoveToTarget(entityCharacter.transform, monster.GetAttackerPosition());
            // Play to end animation
            yield return AttackBetween();
            // Check Collider at end the animation
            this.CauseDamage(GameTag.Enemy);
            // Go back position

            var slotPosition = GetComponent<HeroCharacter>().Slot.GetCharacterPosition();
            yield return MoveToTarget(entityCharacter.transform, slotPosition);

            ResetStateAndCounter();
        }
        private IEnumerator AttackBetween()
        {
            animator.ChangeAnimation(Human_Animator.Slash_State);
            var time = animator.GetAnimationLength(Human_Animator.Slash_State);
            yield return new WaitForSeconds(time);
        }

        private IEnumerator MoveToTarget(Transform Character, Transform TargetTransform)
        {
            animator.ChangeAnimation(Human_Animator.Walk_State);

            while (Vector2.Distance(Character.position, TargetTransform.position) > 0.1f)
            {
                Character.position = Vector2.MoveTowards(Character.position, TargetTransform.position, Speed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }

            animator.ChangeAnimation(Human_Animator.Idle_State);
        }
    }
}