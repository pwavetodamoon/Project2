using CombatSystem.Attack.Utilities;
using Helper;
using System;
using CombatSystem.Entity;
using UnityEngine;

namespace CombatSystem.MonsterAI
{
    public class MonsterNearAI : MonoBehaviour
    {
        public event Action TriggerAttackEvent;
   
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerAttack(collision);
        }

        protected void TriggerAttack(Collider2D collision)
        {
            if (!collision.CompareTag(GameTag.TriggerEvent)) return;
            TriggerAttackEvent?.Invoke();
        }

    }
}