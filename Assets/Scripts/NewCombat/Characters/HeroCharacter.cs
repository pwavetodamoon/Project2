using System.Collections;
using Characters;
using CombatSystem.Data;
using NewCombat.HeroAttack;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.Characters
{
    public class HeroCharacter : EntityCharacter
    {
        public CharacterSlot Slot;
        public HeroNearAttack HeroMeleeAttack;
        public HeroFarAttack HeroRangedAttack;
        protected override void Awake()
        {
            base.Awake();
            attackControl.InitAttackControl(new HeroNearAttack(this));
        }
        [Button]
        public void AttackByType(AttackTypeEnum attackTypeEnum)
        {
            if(attackTypeEnum == AttackTypeEnum.Near)
            {
                attackControl.InitAttackControl(new HeroNearAttack(this));
            }
            else if(attackTypeEnum == AttackTypeEnum.Far)
            {
                attackControl.InitAttackControl(new HeroFarAttack(this));
            }
            else
            {
                Debug.LogError("Attack type is not defined");
            }
        }
        protected override float PlayHurtAnimation()
        {
            var time  = Animator.GetAnimationLength(Human_Animator.Hurt_State);
            Animator.ChangeAnimation(Human_Animator.Hurt_State);
            return time;
        }

        protected override void RegisterObject()
        {
        }

        protected override void RelaseObject()
        {
        }
    }
}
