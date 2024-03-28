using Helper;
using System;
using UnityEngine;

namespace CombatSystem.MonsterAI
{
    public class MonsterNearAI : MonsterAIBase
    {
        public event Action TriggerAttackEvent;

        protected override void TriggerAttack(Collider2D collision)
        {
            if (collision.tag != GameTag.TriggerEvent) return;
            TriggerAttackEvent?.Invoke();
        }

        private void OnDisable()
        {
            TriggerAttackEvent = null;
        }
    }
}