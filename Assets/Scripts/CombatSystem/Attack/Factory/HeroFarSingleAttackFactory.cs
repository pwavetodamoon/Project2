using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Far;
using CombatSystem.Attack.Utilities;
using UnityEngine;

namespace CombatSystem.Attack.Factory
{
    [CreateAssetMenu(fileName = "HeroFarSingleAttackFactory",
        menuName = "SingleAttackFactory/HeroFarSingleAttackFactory")]
    public class HeroFarSingleAttackFactory : HeroSingleAttackFactory
    {
        public RangedProjectileType projectileType;

        public override BaseSingleTargetAttack CreateAttack()
        {
            return new HeroFarAttack(projectileType);
        }
    }
}