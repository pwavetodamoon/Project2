using System.Collections;
using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using CombatSystem.MonsterAI;
using Model.Hero;
using Model.Monsters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem.Attack.Near
{
    public class MonsterNearAttack : BaseMonsterAttack
    {
        private bool triggerAttack = false;
        private IAttackerCounter IAttackerCounter;
        private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        
        public override void GetReference(EntityCharacter newEntityCharacter, Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, attackTransform);
            PlayAnimation(AnimationType.Walk);
            entityCharacter.GetComponent<MonsterNearAI>().TriggerAttackEvent += EnableAttack;
            IAttackerCounter = newEntityCharacter.GetComponent<IAttackerCounter>();
            newEntityCharacter.StartCoroutine(MoveBehaviour());
        }
        protected override IEnumerator StartBehavior()
        {
            if (IsOnTarget() == false) yield break;
            PlayAnimation(AnimationType.Attack);
            yield return new WaitForSeconds(GetAnimationLength(AnimationType.Attack));
            CauseDamage();

            if (IsOnTarget())
            {
                PlayAnimation(AnimationType.Idle);
            }
            else
            {
                PlayAnimation(AnimationType.Walk);
            }

        }
        private void EnableAttack()
        {
            triggerAttack = true;
        }
        protected IEnumerator MoveBehaviour()
        {
            PlayAnimation(AnimationType.Walk);

            while (triggerAttack == false)
            {
                if (CanMoveEntity())
                {
                    MoveDirective(Vector2.left,7);
                }

                yield return waitForEndOfFrame;
            }
            while (true)
            {
                if (CanMoveEntity())
                {
                    var direction = Enemy.GetAttackerTransform().transform.position - entityCharacter.transform.position;
                    MoveDirective(direction.normalized, 5);
                }
                yield return waitForEndOfFrame;
            }

        }

        private bool CanMoveEntity()
        {
            return Enemy != null && !IsOnTarget() && IAttackerCounter.Count == 0;
        }
        private void MoveDirective(Vector2 moveVector, float speed)
        {
            entityCharacter.transform.Translate(moveVector * (Time.deltaTime * speed));
        }

        public float distance;
        private bool IsOnTarget()
        {
            var targetPosition = Enemy.GetAttackerTransform().transform.position;
            distance = Vector2.Distance(entityCharacter.transform.position, targetPosition);
            return distance < .1f;
        }

    }
}