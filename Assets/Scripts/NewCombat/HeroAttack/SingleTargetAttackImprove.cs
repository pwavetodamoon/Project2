using System.Collections;
using NewCombat.AttackedCounting;
using NewCombat.MonsterAI;
using UnityEngine;

namespace NewCombat.HeroAttack
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