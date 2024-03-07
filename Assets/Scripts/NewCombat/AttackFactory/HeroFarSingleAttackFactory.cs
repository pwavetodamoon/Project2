using NewCombat.HeroAttack;
using PrefabFactory;
using UnityEngine;

namespace NewCombat.AttackFactory
{
    [CreateAssetMenu(fileName = "HeroFarSingleAttackFactory", menuName = "SingleAttackFactory/HeroFarSingleAttackFactory")]
    public class HeroFarSingleAttackFactory : HeroSingleAttackFactory
    {
        public ProjectileType projectileType;
        public override BaseSingleTargetAttack CreateAttack()
        {
            return new HeroFarAttack(projectileType);
        }
    }
}