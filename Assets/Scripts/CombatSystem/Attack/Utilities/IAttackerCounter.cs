﻿namespace CombatSystem.Attack.Utilities
{
    public interface IAttackerCounter
    {
        int Count { get; set; }
        void IncreaseAttackerCount();

        void DecreaseAttackerCount();
    }
}