using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using LevelAndStats;
using Model.Hero;
using Model.Monsters;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CombatSystem.Attack.Near
{
    public class MonsterMoveAI
    {
        private MonsterNearAttack monsterNearAttack;
        private Animator_Base animator_Base;
        private EntityCombat attackManager;
        private EntityCharacter Enemy => monsterNearAttack.GetEnemy();
        private EntityCharacter currentEntity => monsterNearAttack.GetEntityCharacter();
        private EntityStats entityStats;
        public bool triggerAttack;
        private float distance;

        public void GetRef(MonsterNearAttack monsterNearAttack, EntityCombat attackManager, Animator_Base animator_Base)
        {
            this.monsterNearAttack = monsterNearAttack;
            this.attackManager = attackManager;
            this.animator_Base = animator_Base;
            entityStats = currentEntity.GetRef<EntityStats>();
            randomPos = CreateNoise();
        }

        private void PlayAnimation(Enum AnimationEnum)
        {
            animator_Base.ChangeAnimation(AnimationEnum);
        }

        public void EnableAttack()
        {
            triggerAttack = true;
        }

        private bool CanMoveEntity()
        {
            return Enemy != null && !IsOnTarget() && attackManager.AttackedByEnemies() == false;
        }

        private void MoveDirective(Vector2 moveVector, float speed)
        {
            currentEntity.transform.Translate(moveVector * (Time.deltaTime * speed));
        }

        public bool IsOnTarget()
        {
            var targetPosition = GetDestinationPosition();
            distance = Vector2.Distance(currentEntity.transform.position, targetPosition);
            return distance < .1f;
        }

        private Vector3 randomPos;

        private Vector3 GetDestinationPosition()
        {
            return Enemy.GetAttackerTransform().transform.position + randomPos;
        }

        private Vector3 CreateNoise()
        {
            var x = Random.Range(-.3f, .3f);
            var y = Random.Range(-.3f, .3f);
            return new Vector3(x, y);
        }

        public IEnumerator MoveBehaviour()
        {
            PlayAnimation(AnimationType.Walk);

            while (triggerAttack == false)
            {
                if (CanMoveEntity() && entityStats.Health() > 0)
                {
                    MoveDirective(Vector2.left, 7);
                }
                else
                {
                    MoveDirective(Vector2.left, 0);
                }

                yield return new WaitForEndOfFrame();
            }
            while (true)
            {
                if (CanMoveEntity())
                {
                    var direction = GetDestinationPosition() - currentEntity.transform.position;
                    MoveDirective(direction.normalized, 5);
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}