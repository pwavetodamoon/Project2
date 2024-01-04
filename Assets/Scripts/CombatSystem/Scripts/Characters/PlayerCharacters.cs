using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerCharacters : CharactersBase
{
    //public static PlayerCharacters Instance;
    bool attacking = false;
    private void Start()
    {

        //data = Game_DataBase.Instance.GetPlayerData(ID);
        
        type = data.AttackType;

        ChangeComponent();
        timeCounter = data.timeCoolDown;
        StartCoroutine(TimeCount());
    }
    private void Update()
    {
        //data.Slot = GetComponentInParent<Transform>();
        //pos = data.Slot;

        //Uu tien danh don manh truoc
        if (timeCounter <= 0 && attacking == false)
        {
            attacking = true;
            CombatManager.AddPlayerAction(Attack);
            timeCounter = data.timeCoolDown + data.animationTime + data.attackTime;
        }
    }
    public override void ChangeComponent()
    {

        if (GetComponent<AttackBase>() != null)
        {
            DestroyImmediate(GetComponent<AttackBase>());
        }
        if (type == AttackTypeEnum.Near)
        {
            transform.AddComponent<ShortRange>();
        }
        if (type == AttackTypeEnum.Far)
        {
            transform.AddComponent<LongRange>();
        }
        var attackBase = GetComponent<AttackBase>();
        attackBase = new AttackBase(data);
        return;

    }
    [Button]
    protected override void Attack()
    {
        //Debug.Log("Attack");
        GetComponent<IAttack>().Attack();
        attacking = false;
    }
    protected override IEnumerator TimeCount()
    {
        while (true)
        {
            //yield return new WaitForSeconds(1);
            timeCounter -= Time.deltaTime;
            yield return null;
        }
    }

}
