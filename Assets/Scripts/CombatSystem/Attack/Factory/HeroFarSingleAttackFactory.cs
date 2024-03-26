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
        public RangedProjectileEnum projectileType;

        public override BaseSingleTargetAttack CreateAttack()
        {
            return new HeroFarAttack(projectileType);
        }
    }
    [CreateAssetMenu(fileName = "HeroMagicAttackFactory",
        menuName = "SingleAttackFactory/HeroMagicAttackFactory")]
    public class HeroMagicAttackFactory : HeroSingleAttackFactory
    {
        public RangedProjectileEnum projectileType;
        public override BaseSingleTargetAttack CreateAttack()
        {
            return new HeroFarMagicAttack(projectileType);
        }
    }
}