using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Near;
using UnityEngine;

namespace CombatSystem.Attack.Factory
{
    [CreateAssetMenu(fileName = "MonsterNearSingleAttackFactory",
        menuName = "SingleAttackFactory/MonsterNearSingleAttackFactory")]
    public class MonsterNearSingleAttackFactory : MonsterSingleAttackFactory
    {
        public override BaseSingleTargetAttack CreateAttack()
        {
            var MonsterMoveAI = new MonsterMoveAI();
            return new MonsterNearAttack(MonsterMoveAI);
        }
    }
}