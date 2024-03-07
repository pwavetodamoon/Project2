using Helper;

namespace NewCombat.HeroAttack
{
    public abstract class BaseMonsterAttack : BaseSingleTargetAttack
    {
        protected override string GetEnemyTag()
        {
            return GameTag.Hero;
        }
    }
}