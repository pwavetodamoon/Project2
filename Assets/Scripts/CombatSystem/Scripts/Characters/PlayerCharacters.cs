using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerCharacters : CharactersBase
{
    //public static PlayerCharacters Instance;


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
        if (timeCounter <= 0)
        {
            
            Attack();
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
        return;

    }
    [Button]
    protected override void Attack()
    {
        Debug.Log("Attack");
        GetComponent<IAttack>().Attack(data);
       
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
