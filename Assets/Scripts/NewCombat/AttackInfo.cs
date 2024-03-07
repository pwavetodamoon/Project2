﻿using NewCombat.Attack.Abstracts;
using NewCombat.Attack.Systems;

namespace NewCombat
{
    public struct AttackInfo
    {
        public AttackInfo(BaseSingleTargetAttack attack, AttackCounter counter)
        {
            this.attack = attack;
            this.counter = counter;
        }

        public BaseSingleTargetAttack attack;
        public AttackCounter counter;

        public bool IsAttackNotNull()
        {
            return attack != null;
        }

        public bool IsCounterNotNull()
        {
            return counter != null;
        }
    }
}