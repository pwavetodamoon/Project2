using System;
using System.Collections;
using Characters;
using Characters.Monsters;
using CombatSystem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
namespace NewCombat.Characters
{
    public class MonsterCharacter : EntityCharacter, IDamageable
    {
        [SerializeField] private Transform AttackedTransform;

        public float Speed = 2;
        public float attackRange = .5f;
        public HeroCharacter Hero;
        public bool notMoving = false;
        public bool IsTrackHero = false;

        bool isStartAttackBehaviour = false;
        void Update()
        {
            Moving();
        }
        private void Moving()
        {
            if(notMoving) return;
            transform.Translate(Vector3.left * (Time.deltaTime * Speed));
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out HeroCharacter hero))
            {
                if (isStartAttackBehaviour == true) return;

                notMoving = true;
                Hero = hero;
                StartCoroutine(MoveToAttkedPos());
                isStartAttackBehaviour = true;
            }
        }
        private void FindTarget()
        {
            if(IsTrackHero == true) return;
            Hero = CombatManager.Instance.GetHero();
            if(Hero == null) return;
            IsTrackHero = true;
        }
        [Button]
        void StartCoroutine()
        {
            StartCoroutine(MoveToAttkedPos());
        }
        [HideInEditorMode]
        public IEnumerator MoveToAttkedPos()
        {
            //if (Hero == null) yield break;
            while (true)
            {
                if(Hero != null)
                {
                    var TargetPosition = Hero.Slot.GetAttackerPosition();
                    var direction = TargetPosition - transform.position;
                    var isOnTarget = false;
                    
                    if (Vector3.Distance(transform.position, TargetPosition) < 0.1f)
                    {
                        isOnTarget = true;
                        allowCounter = true;
                    }
                    else
                    {
                        isOnTarget = false;
                        allowCounter = false;
                    }
                    if (isOnTarget == false)
                    {
                        direction.Normalize();
                        transform.Translate(Speed * Time.deltaTime * direction);
                    }
                }
                else
                {
                    Hero = CombatManager.Instance.GetHero();
                    yield return new WaitForSeconds(.1f);
                }
                yield return new WaitForFixedUpdate();
            }
        }

        public Vector3 GetAttackerPosition()
        {
            if (AttackedTransform == null)
            {
                return transform.position;
            }
            return AttackedTransform.position;
        }
        protected override float PlayHurtAnimation()
        {
            var time = Animator.GetAnimationLength(Monster_Animator.Hurt_State);
            Animator.ChangeAnimation(Monster_Animator.Hurt_State);
            return time;
        }
    }
    public interface IDamageable
    {
        void TakeDamage(float damage);
    }
}
