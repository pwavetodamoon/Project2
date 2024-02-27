using CombatSystem;
using Helper;
using NewCombat.ManagerInEntity;

namespace NewCombat.HeroAttack
{
    public abstract class BaseMonsterAttack : SingleTargetAttackImprove
    {
        protected override string GetEnemyTag()
        {
            return GameTag.Hero;
        }
    }
}