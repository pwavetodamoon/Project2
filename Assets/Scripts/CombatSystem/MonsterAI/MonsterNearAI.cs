using CombatSystem.Attack.Utilities;
using Helper;
using System;
using UnityEngine;

namespace CombatSystem.MonsterAI
{
    public class MonsterNearAI : MonoBehaviour
    {
        [SerializeField] protected EnemyMoving enemyMoving;
        protected Vector2 MoveDirection = Vector2.left;
        private IAttackerCounter AttackerCounter;
        public event Action TriggerAttackEvent;

        private void Awake()
        {
            AttackerCounter = GetComponent<IAttackerCounter>();
        }

        private void OnDisable()
        {
            TriggerAttackEvent = null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerAttack(collision);
        }

        protected void TriggerAttack(Collider2D collision)
        {
            if (collision.tag != GameTag.TriggerEvent) return;
            TriggerAttackEvent?.Invoke();
        }

        protected void Update()
        {
            if (AttackerCounter == null) return;
            if (enemyMoving != null && AttackerCounter.Count <= 0) enemyMoving.Moving(MoveDirection);
        }
    }
}