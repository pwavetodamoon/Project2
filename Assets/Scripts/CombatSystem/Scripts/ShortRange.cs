using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ShortRange : AttackBase, IAttack
{
    public Human_Animator animator;
    void Awake()
    {
        animator = GetComponentInChildren<Human_Animator>();
    }
    public void Attack(BaseData playerData)
    {

        if (IsAttack) return ;
        IsAttack = true;
        StartCoroutine(Near(playerData.animationTime));
        // 3 phase
        // 1. di chuyen den gan enemy
        // 2. tan cong
        // 3. di chuyen ve lai
        IEnumerator Near(float AttackSpeed)
        {
            Vector2 originalPosition = transform.position;
            //var enemyPos = enemyData.Base.transform.position;
            var Enemy = CombatManager.GetEnemyPosition?.Invoke();

            var enemyPos = Enemy == null ? transform.position : Enemy.transform.position;

            // di chuyen
            animator.ChangeState(1);
            yield return transform.DOMove(enemyPos, playerData.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();

            animator.ChangeState(2);
            var timeToAttack = animator.GetCurrentAnimationLength();
            ChangeStateMove(Enemy, false);

            // tan cong
            yield return new WaitForSeconds(timeToAttack);
            animator.ChangeState(1);

            ChangeStateMove(Enemy, true);
            Enemy.health.TakeDamage(playerData.damage);

            //data.weaponPrefab.SetActive(true);

            // di chuyen ve lai
            yield return transform.DOMove(originalPosition, playerData.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
            animator.ChangeState(0);
            IsAttack = false;
            // TODO: Make enemy stop moving in one second
            //data.weaponPrefab.SetActive(false);

        }
    }
    void ChangeStateMove(EnemyCharacters Enemy, bool state)
    {
        if(Enemy == null) return;
        Enemy.moving.isMoving = state;
    }

}
