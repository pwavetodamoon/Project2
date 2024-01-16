using System.Collections;
using Characters;
using CombatSystem;
using NewCombat.Characters;
using UnityEngine;

namespace NewCombat
{
    public class HeroNearAttack : BaseHeroNormalAttack
    {
        [Header("Near Attack Settings")]
        [Min(0.1f)]
        public float GoTime = 1;

        [Min(0.1f)] public float BackTime = 1;

        protected override void OnDrawGizmos()
        {
            if (gizmosTransform == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(gizmosTransform.position, size);
        }

        protected override void CheckCollider()
        {
            if (gizmosTransform == null) return;

            Collider2D[] results = new Collider2D[] { };

            var size1 = Physics2D.OverlapBoxNonAlloc(gizmosTransform.position, size, Angle, results);
            if(size1 == 0) return;
            foreach (var collider in results)
            {
                if (collider.TryGetComponent(out MonsterCharacter monster))
                {
                    Debug.Log("Monster is hit");
                }
            }
        }

        protected override IEnumerator StartBehavior(HeroCharacter hero)
        {
            var monster = CombatManager.Instance.GetMonster();
            if (monster == null)
            {
                Debug.Log("Target is null");
                yield break;
            }
            // Go to enemy
            yield return MoveToTarget(hero.transform, monster.GetAttackerPosition(),GoTime);
            // Play to end animation
            yield return AttackBetween();
            // Check Collider at end the animation
            CheckCollider();
            // Go back position
            yield return MoveToTarget(hero.transform, hero.Slot.GetCharacterPosition(),BackTime);
            
            // Allow attack
            // Reset counter
            IsActive = false;
            attackCounter.ResetCounter();
        }

        private IEnumerator AttackBetween()
        {
            animator.ChangeAnimation(Human_Animator.Slash_State);
            var time = animator.GetAnimationLength(Human_Animator.Slash_State);

            Debug.Log("AttackBetween: " + time);

            yield return new WaitForSeconds(time);

        }

        private IEnumerator MoveToTarget(Transform Character, Vector3 TargetPosition,float time)
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