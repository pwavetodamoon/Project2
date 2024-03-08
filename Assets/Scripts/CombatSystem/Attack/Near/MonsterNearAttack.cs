using System.Collections;
using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using CombatSystem.MonsterAI;
using Model.Monsters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem.Attack.Near
{
    public class MonsterNearAttack : BaseMonsterAttack
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        public override void GetReference(EntityCharacter newEntityCharacter, AnimationManager _animationManager,
            AttackManager attackManager, Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, _animationManager, attackManager, attackTransform);
            entityCharacter.GetComponent<MonsterNearAI>().TriggerAttackEvent += EnableAttack;
            IAttackerCounter = newEntityCharacter.GetComponent<IAttackerCounter>();
            newEntityCharacter.StartCoroutine(MoveBehaviour());
            if (IAttackerCounter != null)
            {
                Debug.Log("IAttackerCounter is not null");
            }
        }
        bool triggerAttack = false;
        [ShowInInspector] private IAttackerCounter IAttackerCounter;
        private void EnableAttack()
        {
            triggerAttack = true;
        }
        protected IEnumerator MoveBehaviour()
        {
            while (triggerAttack == false)
            {
                if (CanMove())
                {
                    MoveDirective(Vector2.left,7);
                }

                yield return waitForEndOfFrame;
            }

            while (true)
            {
                if (CanMove())
                {
                    var direction = Enemy.GetAttackerTransform().transform.position - entityCharacter.transform.position;
                    MoveDirective(direction.normalized, 1);
                }

                yield return waitForEndOfFrame;
            }

        }

        private bool CanMove()
        {
            return Enemy != null && !IsOnTarget() && IAttackerCounter.Count == 0;
        }
        private void MoveDirective(Vector2 moveVector, float speed)
        {
            entityCharacter.transform.Translate(moveVector * (Time.deltaTime * speed));
        }

        private bool IsOnTarget()
        {
            var targetPosition = Enemy.transform.position;
            var distance = Vector2.Distance(entityCharacter.transform.position, targetPosition);
            return distance < .1f;
        }
        protected override IEnumerator StartBehavior()
        {
            if(IsOnTarget() == false) yield break;
            var attackTime = GetAnimationLength(Monster_Animator.AnimationType.Attack);
            PlayAnimation(Monster_Animator.Attack_State);
            yield return new WaitForSeconds(attackTime);
            CauseDamage();
        }
    }
}