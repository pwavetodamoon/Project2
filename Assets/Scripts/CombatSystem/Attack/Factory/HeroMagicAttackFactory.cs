using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Utilities;
using UnityEngine;

namespace CombatSystem.Attack.Factory
{
    [CreateAssetMenu(fileName = "HeroMagicAttackFactory",
        menuName = "SingleAttackFactory/HeroMagicAttackFactory")]
    public class HeroMagicAttackFactory : HeroSingleAttackFactory
    {
        public RangedProjectileType projectileType;

        public override BaseSingleTargetAttack CreateAttack()
        {
            return new HeroFarMagicAttack(projectileType);
        }
    }
}