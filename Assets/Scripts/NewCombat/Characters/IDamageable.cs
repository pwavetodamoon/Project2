using Leveling_System;
using NewCombat.ManagerInEntity;

namespace NewCombat.Characters
{
    public interface IDamageable
    {
        void TakeDamage(EntityStats EntityStats);
    }
}