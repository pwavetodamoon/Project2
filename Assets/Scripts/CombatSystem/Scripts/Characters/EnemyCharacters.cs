using System.Collections;
using UnityEngine;

public class EnemyCharacters : CharactersBase
{
    private EnemyMoving moving;

    private void Awake()
    {
        health = GetComponent<HealthBase>();
        moving = GetComponent<EnemyMoving>();
        health.Setup(data);
        moving.Setup(1);
        ChangeComponent();
    }

    public CharactersBase enemy;

    private IEnumerator StartAttack()
    {
        if (enemy == null)
        {
            Debug.Log("Enemy is null");
            yield break;
        }

        Vector2 originalPosition = transform.position;
        var enemyPos = enemy == null ? transform.position : enemy.transform.position;
        yield return normalAttack.StartCoroutine(normalAttack.GoToEnemy(enemyPos));

        var enemyData = data;
        timeCounter = enemyData.timeCoolDown + enemyData.animationTime + enemyData.attackTime;
        while (true)
        {
            if (!attacking)
                timeCounter -= Time.deltaTime;

            yield return new WaitForEndOfFrame();

            //Debug.Log("Time counter " + timeCounter);
            if (timeCounter <= 0 && attacking == false)
            {
                attacking = true;
                //GetComponentInChildren<Monster_Animator>().ChangeAnimation(1);
                // Add animation here
                yield return normalAttack.StartCoroutine(normalAttack.AttackEnemy());

                enemy.TakeDamage(data.damage);
                //yield return normalAttack.StartCoroutine(normalAttack.GoBackPosition(originalPosition));
                // FIXME: Monster don't stop attack when player is dead
                enemyData = data;
                timeCounter = enemyData.timeCoolDown + enemyData.animationTime + enemyData.attackTime;
                if (enemy.GetHealth() <= 0)
                {
                    enemy = null;
                    break;
                }
                attacking = false;
                // Add animation here
            }
        }
        Debug.Log("End Attack");
    }

    public override void Attack()
    {
        moving.isMoving = false;
        enemy = CombatManager.GetEnemyPosition?.Invoke(1);
        //StartCoroutine(StartAttack());
    }
}