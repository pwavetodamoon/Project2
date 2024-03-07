using NewCombat.HeroAttack;
using UnityEngine;

namespace NewCombat.AttackFactory
{
    public abstract class SingleAttackFactory : ScriptableObject
    {
        public abstract BaseSingleTargetAttack CreateAttack();
    }

    public abstract class HeroSingleAttackFactory : SingleAttackFactory
    {
    }

    public abstract class MonsterSingleAttackFactory : SingleAttackFactory
    {
    }
}