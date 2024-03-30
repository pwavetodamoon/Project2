using CombatSystem.Helper;
using LevelAndStats;
using System;
using System.Collections.Generic;

namespace CombatSystem.Entity.Utilities
{
    [Serializable]
    public class EntityHelper
    {
        public List<EntityStats> List = new();
        public EntityStats EntityStats;
        public float sumOfDamage = 0;

        public EntityHelper(EntityStats entityStats)
        {
            EntityStats = entityStats;
        }

        public void Add(EntityStats EntityStats)
        {
            if (List.Contains(EntityStats) == true) return;
            List.Add(EntityStats);
            CalculatorDamage();
        }

        public void Remove(EntityStats EntityStats)
        {
            if (List.Contains(EntityStats) == false) return;
            List.Remove(EntityStats);
            CalculatorDamage();
        }


        public float CalculatorDamage()
        {
            sumOfDamage = 0;
            foreach (var entity1 in List)
            {
                var damage = EntityStatsHelp.CalculatorFinalDamage(EntityStats, entity1);
                sumOfDamage += damage;
            }

            return sumOfDamage;
        }
    }
}