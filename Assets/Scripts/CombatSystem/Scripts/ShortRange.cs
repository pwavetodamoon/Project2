using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ShortRange : AttackBase, IAttack
{
    public Human_Animator animator;
    // TODO: Make Attack for monster 
    void Awake()
    {
        animator = GetComponentInChildren<Human_Animator>();
    }
    public void Attack()
    {

        if (IsAttack) return;
        IsAttack = true;
        StartCoroutine(Near());
        // 3 phase
        // 1. di chuyen den gan enemy
        // 2. tan cong
        // 3. di chuyen ve lai
        IEnumerator Near()
        {
            var _base = GetComponent<CharactersBase>();
            CharactersBase Enemy = null;
            if(_base is PlayerCharacters)
                Enemy = CombatManager.GetEnemyPosition?.Invoke(0);
            else if(_base is EnemyCharacters)
                Enemy = CombatManager.GetEnemyPosition?.Invoke(1);


            Vector2 originalPosition = transform.position;
            //var enemyPos = enemyData.Base.transform.position;
            var enemyPos = Enemy == null ? transform.position : Enemy.transform.position;

            yield return StartCoroutine(GoToEnemy(enemyPos));

            yield return StartCoroutine(AttackEnemy());

            //ChangeStateMove(Enemy, false);
            yield return StartCoroutine(GoBackPosition(originalPosition));
            //ChangeStateMove(Enemy, true);

            Enemy.TakeDamage(data.damage);

            IsAttack = false;
            // TODO: Make enemy stop moving in one second
            //data.weaponPrefab.SetActive(false);

        }
    }
    IEnumerator GoToEnemy(Vector2 enemyPos)
    {
        // di chuyen
        //animator.ChangeState(1);
        yield return transform.DOMove(enemyPos, data.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
    }
    IEnumerator AttackEnemy()
    {
        // tan cong
        yield return new WaitForSeconds(data.attackTime);
    }
    IEnumerator GoBackPosition(Vector2 originalPosition)
    {
        // di chuyen ve lai
        yield return transform.DOMove(originalPosition, data.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
        yield return null;
    }
    void ChangeStateMove(EnemyCharacters Enemy, bool state)
    {
        if (Enemy == null) return;
        Enemy.moving.isMoving = state;
    }

}

