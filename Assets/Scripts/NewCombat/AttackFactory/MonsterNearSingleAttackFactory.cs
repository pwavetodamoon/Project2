using NewCombat.HeroAttack;
using NewCombat.MonsterAttack;
using UnityEngine;

namespace NewCombat.AttackFactory
{
    [CreateAssetMenu(fileName = "MonsterNearSingleAttackFactory", menuName = "SingleAttackFactory/MonsterNearSingleAttackFactory")]
    public class MonsterNearSingleAttackFactory : MonsterSingleAttackFactory
    {
        public override BaseSingleTargetAttack CreateAttack()
        {
            return new MonsterNearAttack();
        }
    }
}