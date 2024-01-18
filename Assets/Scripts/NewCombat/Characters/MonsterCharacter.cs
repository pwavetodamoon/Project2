using System;
using System.Collections;
using Characters.Monsters;
using UnityEngine;
namespace NewCombat.Characters
{
    public class MonsterCharacter : EntityCharacter, IDamageable
    {
        [SerializeField] private Transform AttackedTransform;

        public float speed = 2;
        public float attackRange = .5f;
        public HeroCharacter Hero;
        private void Moving()
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Hero"))
            {
                Attack();
            }
        }
        private void Attack()
        {

        }
        public Vector3 GetAttackerPosition()
        {
            if (AttackedTransform == null)
            {
                return transform.position;
            }
            return AttackedTransform.position;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            Animator.ChangeAnimation(Monster_Animator.Hurt_State);
        }
    }
    public interface IDamageable
    {
        void TakeDamage(float damage);
    }
}
