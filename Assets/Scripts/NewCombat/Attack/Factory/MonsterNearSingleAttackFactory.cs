using NewCombat.Attack.Abstracts;
using NewCombat.Attack.Near;
using UnityEngine;

namespace NewCombat.Attack.Factory
{
    [CreateAssetMenu(fileName = "MonsterNearSingleAttackFactory",
        menuName = "SingleAttackFactory/MonsterNearSingleAttackFactory")]
    public class MonsterNearSingleAttackFactory : MonsterSingleAttackFactory
    {
        public override BaseSingleTargetAttack CreateAttack()
        {
            return new MonsterNearAttack();
        }
    }
}