using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using Helper;
using UnityEngine;

namespace CombatSystem.MonsterAI
{
    public class MonsterNearAI : MonsterAIBase
    {
        [SerializeField] private float Speed = 2;

        private Vector3 AttackerPositionNoise = Vector3.zero;
        private Transform AttackHeroTransform;

        private AttackManager attackManager;
        private float distance;
        private EntityCharacter Enemy;
        public bool CanAttack { get; private set; }

        protected override void Update()
        {
            base.Update();
            DefineAIBehaviour();
        }

        protected override void TriggerAttack(Collider2D collision)
        {
            if (collision.tag != GameTag.TriggerEvent) return;
            IsTriggerAttackBehaviour = true;
        }

        public void SetEnemy(EntityCharacter enemy)
        {
            Enemy = enemy;
            AttackHeroTransform = enemy.GetAttackerTransform();
        }

        protected void DefineAIBehaviour()
        {
            if (Enemy == null) return;

            if (IsOnTarget())
            {
                Debug.Log("Haha 2");
                LogicWhenOnTarget();
            }
            else
            {
                Debug.Log("Haha 1");

                LogicWhenNotOnTarget();
                CheckMoveBehaviour();
            }
        }

        public void AddPositionNoise(Vector3 noise)
        {
            AttackerPositionNoise = noise;
        }

        private void CheckMoveBehaviour()
        {
            if (IsTriggerAttackBehaviour)
            {
                MoveDirection = DirectionToTransform(AttackHeroTransform.transform);
                Speed = 1.5f;
            }
            else
            {
                MoveDirection = Vector2.left;
                Speed = 2;
            }
        }

        private void LogicWhenOnTarget()
        {
            attackManager.SetTimeCounterValue(true);
            MoveDirection = Vector2.zero;
            CanAttack = true;
        }

        private void LogicWhenNotOnTarget()
        {
            CanAttack = false;
            attackManager.SetTimeCounterValue(false);
        }

        private bool IsOnTarget()
        {
            var targetPosition = AttackHeroTransform.position + AttackerPositionNoise;
            distance = Vector2.Distance(transform.position, targetPosition);
            return distance < .1f;
        }

        private Vector2 DirectionToTransform(Transform enemy)
        {
            var targetPosition = enemy.position + AttackerPositionNoise;
            var direction = targetPosition - transform.position;
            direction.Normalize();
            return direction;
        }

        public void InitializeComponents()
        {
            attackManager = GetComponent<AttackManager>();
            enemyMoving = GetComponent<EnemyMoving>();
        }
    }
}