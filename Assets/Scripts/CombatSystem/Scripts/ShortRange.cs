using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ShortRange : AttackBase, IAttack
{
    EnemyData enemyData; // TODO: FIX THIS

    public void Attack(BaseData playerData)
    {

        if (IsAttack) return ;
        IsAttack = true;
        StartCoroutine(Near(playerData.animationTime));

        IEnumerator Near(float AttackSpeed)
        {
            Vector2 originalPosition = transform.position;
            //var enemyPos = enemyData.Base.transform.position;
            var enemyPos = Vector2.zero;// FIXME: enemyData.Base.transform.position;
            var Enemy = CombatManager.GetEnemyPosition?.Invoke();
            enemyPos = Enemy.transform.position;
            yield return transform.DOMove(enemyPos, playerData.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();

            Enemy.moving.isMoving = false;

            yield return new WaitForSeconds(AttackSpeed);
            //data.weaponPrefab.SetActive(true);
            yield return transform.DOMove(originalPosition, playerData.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
            IsAttack = false;
            // TODO: Make enemy stop moving in one second
            Enemy.moving.isMoving = true;
            //data.weaponPrefab.SetActive(false);

        }
        
        

    }

}
