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
            //Slot = GetComponentInParent<CharacterSlot>();
        }

        [Button]
        public void AttackByType(AttackTypeEnum attackTypeEnum)
        {
            if(attackTypeEnum == AttackTypeEnum.Near)
            {
                GetComponent<HeroNearAttack>().ExecuteAttack();
            }
            else if(attackTypeEnum == AttackTypeEnum.Far)
            {
                GetComponent<HeroFarAttack>().ExecuteAttack();
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
    }
}
