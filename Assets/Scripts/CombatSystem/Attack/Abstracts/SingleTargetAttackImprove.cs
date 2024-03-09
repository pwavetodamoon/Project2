using CombatSystem.Attack.Utilities;
using UnityEngine;

namespace CombatSystem.Attack.Abstracts
{
    public abstract class SingleTargetAttackImprove : BaseSingleTargetAttack
    {
        private IAttackerCounter IAttackerCounter;

        protected void IncreaseAttackerCount()
        {
            Debug.Log("+1", entityCharacter.gameObject);
            IAttackerCounter = Enemy.GetComponent<IAttackerCounter>();
            IAttackerCounter?.IncreaseAttackerCount();
        }

        protected override void ResetStateAndCounter()
        {
            base.ResetStateAndCounter();
            IAttackerCounter?.DecreaseAttackerCount();
        }
    }
}