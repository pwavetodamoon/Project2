using CombatSystem;
using NewCombat.ManagerInEntity;
using NewCombat.MonsterAI;
using System.Collections;
using Helper;
using Leveling_System;
using NewCombat.Helper;

namespace NewCombat.HeroAttack
{
    public abstract class BaseHeroAttack : SingleTargetAttackImprove
    {
        protected override IEnumerator StartBehavior()
        {
            yield return base.StartBehavior();
            var enemyStats = Enemy.GetComponent<EntityStats>();

            var damageOfEnemy = EntityStatsHelp.CalculatorFinalDamage(EntityStats, enemyStats);
            if (enemyStats.Health - damageOfEnemy <= 0)
            {
                CombatEntitiesManager.Instance.RemoveEntityByTag(Enemy, GetEnemyTag());
            }
            yield return null;
        }

        protected override string GetEnemyTag()
        {
            return GameTag.Enemy;
        }
    }

    // Create BaseMonsterAttack.cs like BaseHeroAttack.cs
}