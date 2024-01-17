using Characters;
using CombatSystem.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.Characters
{
    public class HeroCharacter : EntityCharacter
    {
        public CharacterSlot Slot;
        public HeroNearAttack HeroMeleeAttack;
        public HeroFarAttack HeroRangedAttack;
        public bool allowExcuteAnotherAttack = true;
        protected override void Awake()
        {
            base.Awake();
            Slot = GetComponentInParent<CharacterSlot>();
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
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            Animator.ChangeAnimation(Human_Animator.Hurt_State);
        }
    }
}
