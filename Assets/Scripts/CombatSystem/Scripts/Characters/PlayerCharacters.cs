using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerCharacters : CharactersBase
{
    //public static PlayerCharacters Instance;
    public bool Test = false;
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
        if(Test == true)
        {
            return;
        }
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
    [Button]
    protected override void Attack()
    {
        //Debug.Log("Attack");
        GetComponent<IAttack>().Attack();
        attacking = false;
    }

    public override IEnumerator TimeCount()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (attacking == false && timeCounter > 0)
                timeCounter -= Time.deltaTime;
        }
    }
}
