using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ShortRange : AttackBase, IAttack
{
    // TODO: FIX THIS
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

        IEnumerator Near(float AttackSpeed)
        {
            Vector2 originalPosition = transform.position;
            //var enemyPos = enemyData.Base.transform.position;
            var Enemy = CombatManager.GetEnemyPosition?.Invoke();

            var enemyPos = Enemy == null ? transform.position : Enemy.transform.position;

            // di chuyen
            yield return transform.DOMove(enemyPos, playerData.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();

            // tan cong
            animator.PlayAnimation(Human_Animator.AnimationType.Slash);

            ChangeStateMove(Enemy, false);
            yield return new WaitForSeconds(0.63f);
            ChangeStateMove(Enemy, true);
            Enemy.health.TakeDamage(playerData.damage);

            //data.weaponPrefab.SetActive(true);

            // di chuyen ve lai
            animator.PlayAnimation(Human_Animator.AnimationType.Idle);
            yield return transform.DOMove(originalPosition, playerData.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
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
