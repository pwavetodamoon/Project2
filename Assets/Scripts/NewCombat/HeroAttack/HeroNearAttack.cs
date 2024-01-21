using System.Collections;
using Characters;
using CombatSystem;
using NewCombat.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.HeroAttack
{
    public class HeroNearAttack : BaseHeroNormalAttack
    {

        public Transform gizmosTransform;
        public Vector2 gizmosPosition;
        public Vector2 size = Vector3.one;
        
        protected float Angle = 0;
        public float Speed = 2;
        public HeroCharacter hero;

        protected override void Awake()
        {
            base.Awake();
            hero = GetComponent<HeroCharacter>();
        }
        protected void OnDrawGizmos()
        {
            if (gizmosTransform == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(gizmosTransform.position, size);
        }
        public string Tag = "Enemy";
        protected override IEnumerator StartBehavior()
        {
            var monster = CombatManager.Instance.GetMonster();
            if (monster == null)
            {
                IsActive = false;
                yield break;
            }
            // Go to enemy
            hero.allowExcuteAnotherAttack = false;
            
            yield return MoveToTarget(hero.transform, monster.GetAttackerPosition());
            // Play to end animation
            yield return AttackBetween();
            // Check Collider at end the animation
            this.CauseDamage(Tag);
            // Go back position
            yield return MoveToTarget(hero.transform, hero.Slot.GetCharacterPosition());

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

            while (Vector2.Distance(Character.position, TargetPosition) > 0.1f)
            {
                Character.position = Vector2.MoveTowards(Character.position, TargetPosition, Speed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }

            animator.ChangeAnimation(Human_Animator.Idle_State);
        }
        protected override void CauseDamage(string Tag)
        {
            CombatCollider.CheckOverlapBox(Tag, gizmosTransform.position, size, Angle);
        }
    }
}