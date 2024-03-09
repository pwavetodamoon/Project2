using CombatSystem.Attack.Systems;
using CombatSystem.Attack.Utilities;
using Model.Hero;
using Model.Monsters;
using UnityEngine;

namespace CombatSystem.Entity.Utilities
{
    public class AttackManager : MonoBehaviour, IAttackerCounter
    {
        [SerializeField] protected bool allowCounter = true;
        [SerializeField] protected bool allowExecuteAnotherAttack = true;
        private Animator_Base animator_base;
        private AttackControl attackControl;

        private void Awake()
        {
            attackControl = GetComponent<AttackControl>();
            animator_base = GetComponentInChildren<Animator_Base>();
        }

        public int Count { get; set; }

        public void IncreaseAttackerCount()
        {
            //if (attackControl.IsAttacking() == false)
            //{
            //    animator_base.ChangeAnimation(AnimationType.Hurt);
            //}
            Count++;
        }

        public void DecreaseAttackerCount()
        {
            Count--;
            if (Count < 0) Count = 0;
        }

        public bool AttackedByEnemies()
        {
            return Count > 0;
        }

        public bool IsAllowAttack()
        {
            return allowExecuteAnotherAttack;
        }

        public bool IsAllowCounter()
        {
            return allowCounter;
        }

        public bool SetAllowExecuteAttackValue(bool value)
        {
            return allowExecuteAnotherAttack = value;
        }

        public bool SetTimeCounterValue(bool value)
        {
            return allowCounter = value;
        }
    }
}