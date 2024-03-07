using NewCombat.Attack.Abstracts;
using UnityEngine;

namespace NewCombat.Attack.Factory
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