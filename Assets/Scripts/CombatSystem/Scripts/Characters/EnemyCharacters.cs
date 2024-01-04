using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCharacters : CharactersBase
{
    // TODO: (Complete player health) Add player health and monster attack
    public EnemyMoving moving;

    public float speed;

    private void Start()
    {
        health = GetComponent<HealthBase>();
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
        GetComponent<IAttack>().Attack();
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
