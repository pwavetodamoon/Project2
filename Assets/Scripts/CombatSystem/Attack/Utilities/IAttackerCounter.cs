using CombatSystem.Entity;
using LevelAndStats;

namespace CombatSystem.Attack.Utilities
{
    public interface IAttackerCounter
    {
        int Count { get; set; }
        void IncreaseAttackerCount(EntityStats entityStats);

        void DecreaseAttackerCount(EntityStats entityStats);

        bool IsOutOfHealth();

    }
}