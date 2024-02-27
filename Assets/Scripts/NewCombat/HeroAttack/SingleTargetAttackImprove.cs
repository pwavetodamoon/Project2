using System.Collections;
using NewCombat.AttackedCounting;
using NewCombat.MonsterAI;

namespace NewCombat.HeroAttack
{
    public abstract class SingleTargetAttackImprove : BaseSingleTargetAttack
    {
        private IAttackerCounter IAttackerCounter;

        protected override IEnumerator StartBehavior()
        {
            IAttackerCounter = Enemy.GetComponent<IAttackerCounter>();
            IAttackerCounter?.IncreaseAttackerCount();
            yield return null;
        }
        protected override void ResetStateAndCounter()
        {
            base.ResetStateAndCounter();
            IAttackerCounter?.DecreaseAttackerCount();
        }
    }
}