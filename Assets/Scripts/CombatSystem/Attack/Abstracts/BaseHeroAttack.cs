using System.Collections;
using CombatSystem.Helper;
using Helper;
using LevelAndStats;
using UnityEngine;

namespace CombatSystem.Attack.Abstracts
{
    public abstract class BaseHeroAttack : SingleTargetAttackImprove
    {
        
        protected override string GetEnemyTag()
        {
            return GameTag.Enemy;
        }
    }

    // Create BaseMonsterAttack.cs like BaseHeroAttack.cs
}