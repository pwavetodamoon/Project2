using NewCombat.HeroAttack;
using UnityEngine;

namespace NewCombat.AttackFactory
{
    [CreateAssetMenu(fileName = "HeroFarSingleAttackFactory", menuName = "SingleAttackFactory/HeroFarSingleAttackFactory")]
    public class HeroFarSingleAttackFactory : HeroSingleAttackFactory
    {
        public override BaseSingleTargetAttack CreateAttack()
        {
            return new HeroFarAttack();
        }
    }
}