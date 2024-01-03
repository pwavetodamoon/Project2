using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCharacters : CharactersBase
{
    //public static EnemyCharacters Instance;
    public EnemyHealth health;
    public EnemyMoving moving;

    public float speed;

    private void Start()
    {
        health = GetComponent<EnemyHealth>();
        moving = GetComponent<EnemyMoving>();

        speed = 1;
        health.Setup(data);
        moving.Setup(speed);

        ChangeComponent();
        type = data.AttackType;
        //data.Base = transform;
        StartCoroutine(TimeCount());
    }
    private void Update()
    {
        if (timeCounter == 0)
        {
            //Attack();
            timeCounter = data.timeCoolDown;
        }
    }
    protected override void Attack()
    {
        GetComponent<IAttack>().Attack(data);
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
        //GetComponent<AttackBase>().enemyData = Game_DataBase.Instance.GetEnemyData(ID);

    }
    protected override IEnumerator TimeCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeCounter--;
            yield return null;
        }
    }

}
