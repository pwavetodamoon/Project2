using NewCombat.AttackedCounting;
using NewCombat.MonsterAI;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewCombat.ManagerInEntity
{
    public class AttackManager : MonoBehaviour, IAttackerCounter
    {
        [SerializeField] protected bool allowCounter = true;
        [SerializeField] protected bool allowExecuteAnotherAttack = true;

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

        public int Count { get; set; }
        public void IncreaseAttackerCount()
        {
            Count++;
        }

        public void DecreaseAttackerCount()
        {
            Count--;
            if (Count < 0)
            {
                Count = 0;
            }
        }
    }
}