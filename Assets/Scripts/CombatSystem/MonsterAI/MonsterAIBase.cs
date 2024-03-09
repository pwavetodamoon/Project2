using CombatSystem.Attack.Utilities;
using UnityEngine;

namespace CombatSystem.MonsterAI
{
    public abstract class MonsterAIBase : MonoBehaviour
    {
        [SerializeField] protected EnemyMoving enemyMoving;
        public bool IsTriggerAttackBehaviour;
        public IAttackerCounter AttackerCounter;
        protected Vector2 MoveDirection = Vector2.left;

        private void Awake()
        {
            AttackerCounter = GetComponent<IAttackerCounter>();
        }

        protected virtual void Update()
        {
            if (AttackerCounter == null) return;
            if (enemyMoving != null && AttackerCounter.Count <= 0) enemyMoving.Moving(MoveDirection);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerAttack(collision);
        }

        protected abstract void TriggerAttack(Collider2D collision);
    }
}