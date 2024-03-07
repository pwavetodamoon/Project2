using NewCombat.HeroAttack;
using UnityEngine;

namespace NewCombat.AttackFactory
{
    [CreateAssetMenu(fileName = "HeroNearSingleAttackFactory",
        menuName = "SingleAttackFactory/HeroNearSingleAttackFactory")]
    public class HeroNearSingleAttackFactory : HeroSingleAttackFactory
    {
        public override BaseSingleTargetAttack CreateAttack()
        {
            return new HeroNearAttack();
        }
    }
}