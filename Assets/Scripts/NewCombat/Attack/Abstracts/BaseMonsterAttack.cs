using Helper;

namespace NewCombat.Attack.Abstracts
{
    public abstract class BaseMonsterAttack : BaseSingleTargetAttack
    {
        protected override string GetEnemyTag()
        {
            return GameTag.Hero;
        }
    }
}