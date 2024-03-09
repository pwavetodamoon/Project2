using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Near;
using UnityEngine;

namespace CombatSystem.Attack.Factory
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