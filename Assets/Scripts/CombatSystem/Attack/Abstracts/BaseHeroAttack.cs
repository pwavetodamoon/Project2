using System.Collections;
using CombatSystem.Helper;
using Helper;
using LevelAndStats;
using UnityEngine;

namespace CombatSystem.Attack.Abstracts
{
    public abstract class BaseHeroAttack : SingleTargetAttackImprove
    {
        protected override IEnumerator StartBehavior()
        {
            IncreaseAttackerCount();
            var enemyStats = Enemy.GetComponent<EntityStats>();

            var damageOfEnemy = EntityStatsHelp.CalculatorFinalDamage(EntityStats, enemyStats);
            if (enemyStats.Health() - damageOfEnemy <= 0)
            {
                Debug.Log("Enemy is dead");
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