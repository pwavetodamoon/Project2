using System.Collections;
using Characters;
using CombatSystem;
using CombatSystem.ActionCommand;
using NewCombat.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat
{
    public class HeroNearAttack : BaseHeroNormalAttack
    {

        protected override void OnDrawGizmos()
        {
            if (gizmosTransform == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(gizmosTransform.position, size);
        }

        protected override void CheckCollider()
        {
            if (gizmosTransform == null) return;


            var results = Physics2D.OverlapBoxAll(gizmosTransform.position, size, Angle);
            if(results.Length == 0) return;
            foreach (var collider in results)
            {
                if (collider.TryGetComponent(out MonsterCharacter monster))
                {
                    collider.GetComponent<IDamageable>().TakeDamage(Hero.BaseStats.Strength);
                }
            }
        }

        protected override IEnumerator StartBehavior()
        {
            var monster = CombatManager.Instance.GetMonster();
            if (monster == null)
            {
                IsActive = false;
                yield break;
            }
            // Go to enemy
            Hero.allowExcuteAnotherAttack = false;
            
            yield return MoveToTarget(Hero.transform, monster.GetAttackerPosition());
            // Play to end animation
            yield return AttackBetween();
            // Check Collider at end the animation
            CheckCollider();
            // Go back position
            yield return MoveToTarget(Hero.transform, Hero.Slot.GetCharacterPosition());

            ResetStateAndCounter();
        }
        private IEnumerator AttackBetween()
        {
            animator.ChangeAnimation(Human_Animator.Slash_State);
            var time = animator.GetAnimationLength(Human_Animator.Slash_State);
            yield return new WaitForSeconds(time);
        }

        private IEnumerator MoveToTarget(Transform Character, Vector3 TargetPosition)
        {
            animator.ChangeAnimation(Human_Animator.Walk_State);

            while (true)
            {
                var direction = TargetPosition - Character.position;
                direction.Normalize();
                Character.Translate(Speed * Time.deltaTime * direction);

                if (Vector3.Distance(Character.position, TargetPosition) < 0.1f)
                {
                    break;
                }

                yield return new WaitForFixedUpdate();
            }
            animator.ChangeAnimation(Human_Animator.Idle_State);
        }
    }
}