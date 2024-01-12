using Sirenix.OdinInspector;
using UnityEngine;


public class HeroCharacter : MonoBehaviour
{
    public CharacterSlot Slot;
    public Animator_Base Animator;

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
            GetComponent<HeroMeleeAttack>().ExecuteAttack(Animator);
        }
        else if(attackTypeEnum == AttackTypeEnum.Far)
        {
            GetComponent<HeroRangedAttack>().ExecuteAttack(Animator);
        }
        else
        {
            Debug.LogError("Attack type is not defined");
        }
    }
}
