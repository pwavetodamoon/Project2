using NewCombat.Attack.Abstracts;
using NewCombat.Attack.Far;
using NewCombat.Attack.Utilities;
using UnityEngine;

namespace NewCombat.Attack.Factory
{
    [CreateAssetMenu(fileName = "HeroFarSingleAttackFactory",
        menuName = "SingleAttackFactory/HeroFarSingleAttackFactory")]
    public class HeroFarSingleAttackFactory : HeroSingleAttackFactory
    {
        public ProjectileType projectileType;

        public override BaseSingleTargetAttack CreateAttack()
        {
            return new HeroFarAttack(projectileType);
        }
    }
}