using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ShortRange : AttackBase, IAttack
{
    Enemy enemyData; // TODO: FIX THIS

    public void Attack(BaseData playerData)
    {

        //Debug.Log("Enemypos" + enemyData.enemyPos.transform.position);
        //Debug.Log("PlayerAttackSpeed" + playerData.attackTime);
        //Debug.Log(playerData.Base.transform.position);
        //Debug.Log(playerData.attackTime);

        if (IsAttack) return ;
        IsAttack = true;
        StartCoroutine(Near(playerData.animationTime));

        IEnumerator Near(float AttackSpeed)
        {
            Vector2 originalPosition = transform.position;
            //var enemyPos = enemyData.Base.transform.position;
            var enemyPos = Vector2.zero;// FIXME: enemyData.Base.transform.position;


            yield return transform.DOMove(enemyPos, playerData.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
            yield return new WaitForSeconds(AttackSpeed);
            //playerData.weaponPrefab.SetActive(true);
            yield return transform.DOMove(originalPosition, playerData.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
            IsAttack = false;
            //playerData.weaponPrefab.SetActive(false);

        }
        
        

    }

}
