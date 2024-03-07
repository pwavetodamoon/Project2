using NewCombat.Attack.Abstracts;
using NewCombat.Attack.Near;
using UnityEngine;

namespace NewCombat.Attack.Factory
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