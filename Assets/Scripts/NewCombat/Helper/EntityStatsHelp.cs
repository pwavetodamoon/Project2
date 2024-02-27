using Leveling_System;
using NewCombat.ManagerInEntity;

namespace NewCombat.Helper
{
    public static class EntityStatsHelp
    {
        public static float CalculatorFinalDamage(EntityStats entity, EntityStats enemy)
        {
            var myDmgResistant = CalculatorDamageResistance(entity,enemy);
            return DamageCanDealing(enemy.GetDamage(),myDmgResistant);
        }
        private static float DamageCanDealing(float damage,float DamageResistance)
        {
            return damage * (1 - DamageResistance / (DamageResistance + 100));
        }
        private static int CalculatorDamageResistance(EntityStats entity,EntityStats enemy)
        {
            var diffenceLevel = enemy.Level - entity.Level;
            return diffenceLevel * 5;
        }
    }
}