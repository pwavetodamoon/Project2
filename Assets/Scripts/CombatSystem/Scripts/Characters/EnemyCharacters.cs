using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCharacters : CharactersBase
{
    EnemyMoving moving;
    private void Awake()
    {
        health = GetComponent<HealthBase>();
        moving = GetComponent<EnemyMoving>();
        health.Setup(data);
        moving.Setup(1);
        ChangeComponent();
    }
    public CharactersBase enemy;

    IEnumerator StartAttack()
    {
        if(enemy == null)
        {
            Debug.Log("Enemy is null");
            yield break;
        }

        Vector2 originalPosition = transform.position;
        var enemyPos = enemy == null ? transform.position : enemy.transform.position;
        yield return normalAttack.StartCoroutine(normalAttack.GoToEnemy(enemyPos));
        timeCounter = data.timeCoolDown + data.animationTime + data.attackTime;
        while (true)
        {
            if(!attacking)
                timeCounter -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
            
            Debug.Log("Time counter " + timeCounter);   
            if (timeCounter <= 0 && attacking == false)
            {
                attacking = true;
                yield return normalAttack.StartCoroutine(normalAttack.AttackEnemy());
                enemy.TakeDamage(data.damage);
                //yield return normalAttack.StartCoroutine(normalAttack.GoBackPosition(originalPosition));
                timeCounter = data.timeCoolDown + data.animationTime + data.attackTime;
                if (enemy.GetHealth() <= 0)
                {
                    enemy = null;
                    break;
                }
                attacking = false;

            }
        }
        Debug.Log("End Attack");
    }
    public override void Attack()
    {
        moving.isMoving = false;
        enemy = CombatManager.GetEnemyPosition?.Invoke(1);
        StartCoroutine(StartAttack());

    }
}
