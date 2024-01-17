using System;
using System.Collections;
using Characters.Monsters;
using UnityEngine;
namespace NewCombat.Characters
{
    public class MonsterCharacter : EntityCharacter, IDamageable
    {
        [SerializeField] private Transform AttackedTransform;
        public Vector3 GetAttackerPosition()
        {
            if(AttackedTransform == null)
            {
                return transform.position;
            }
            return AttackedTransform.position;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            // StartCoroutine(Hurt());
            Animator.ChangeAnimation(Monster_Animator.Hurt_State);
        }

        private IEnumerator Hurt()
        {
            Animator.ChangeAnimation(Monster_Animator.Hurt_State);
            var time = Animator.GetAnimationLength(Monster_Animator.Hurt_State);
            yield return new WaitForSeconds(time);
            Animator.ChangeAnimation(Monster_Animator.Walk_State);
        }
    }
    public interface IDamageable
    {
        void TakeDamage(float damage);
    }
}
