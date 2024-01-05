using DG.Tweening;
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
        health = GetComponent<HealthBase>();
        health.Setup(data);
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
    public override void Attack()
    {
        StartCoroutine(StartAttack());
    }
    [Button]
    IEnumerator StartAttack()
    {
        var Enemy = CombatManager.GetEnemyPosition?.Invoke(0);
        Vector2 originalPosition = transform.position;
        var enemyPos = Enemy == null ? transform.position : Enemy.transform.position;
        yield return normalAttack.StartCoroutine(normalAttack.GoToEnemy(enemyPos));
        yield return normalAttack.StartCoroutine(normalAttack.AttackEnemy());
        Enemy.TakeDamage(data.damage);
        yield return normalAttack.StartCoroutine(normalAttack.GoBackPosition(originalPosition));
        attacking = false;
    }

}
