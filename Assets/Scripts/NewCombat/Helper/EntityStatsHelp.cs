using Leveling_System;

namespace NewCombat.Helper
{
    public static class EntityStatsHelp
    {
        public static float CalculatorFinalDamage(EntityStats entity, EntityStats enemy)
        {
            var myDmgResistant = CalculatorDamageResistance(entity, enemy);
            return DamageCanDealing(enemy.GetDamage(), myDmgResistant);
        }

        private static float DamageCanDealing(float damage, float damageResistance)
        {
            return damage * (1 - damageResistance / (damageResistance + 100));
        }

        private static int CalculatorDamageResistance(EntityStats entity, EntityStats enemy)
        {
            var levelOffset = enemy.Level() - entity.Level();
            return levelOffset * 5;
        }
    }
}