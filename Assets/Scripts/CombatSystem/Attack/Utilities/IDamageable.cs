using LevelAndStats;

namespace CombatSystem.Attack.Utilities
{
    public interface IDamageable
    {
        void TakeDamage(EntityStats EntityStats);
    }
}