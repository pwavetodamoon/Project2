using Characters.Monsters;
using CombatSystem.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.Characters
{
    public class HeroCharacter : MonoBehaviour
    {
        public CharacterSlot Slot;
        public Animator_Base Animator;
        public HeroNearAttack HeroMeleeAttack;
        public HeroFarAttack HeroRangedAttack;
        public bool allowExcuteAnotherAttack = true;
        private void Awake()
        {
            Slot = GetComponentInParent<CharacterSlot>();
            Animator = GetComponentInChildren<Animator_Base>();
        }
        [Button]
        //public void Attack()
        //{
        //    GetComponent<IHeroAttack>().ExecuteAttack(Animator);
        //}
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
    }
}
