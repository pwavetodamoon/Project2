using Sirenix.OdinInspector;
using UnityEngine;


public class HeroCharacter : MonoBehaviour
{
    public CharacterSlot Slot;
    public Animator_Base Animator;
    public HeroMeleeAttack HeroMeleeAttack;
    public HeroRangedAttack HeroRangedAttack;
    private void Awake()
    {
        Slot = GetComponentInParent<CharacterSlot>();
        Animator = GetComponentInChildren<Animator_Base>();
    }
    [SerializeField] private float timeCounter;
    [SerializeField] private float maxTime = 0.5f;
    [SerializeField] private int attackCount;
    [SerializeField] private int maxAttackCount = 3;
    private void Update()
    {
        //if (timeCounter > 0 && HeroMeleeAttack.isActive == false)
        //    timeCounter -= Time.deltaTime;
        //else
        //{
        //    timeCounter = maxTime;
        //    HeroMeleeAttack.ExecuteAttack(Animator);
        //}
        if(attackCount >= maxAttackCount && HeroRangedAttack.isActive == false)
        {
            HeroRangedAttack.ExecuteAttack(Animator);
            attackCount = 0;
        }
    }
    public void CountingAttack()
    {
        attackCount++;
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
