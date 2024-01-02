using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ShortRange : AttackBase, IAttack
{

    public void Attack()
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
            
            yield return transform.DOMove(enemyData.Base.transform.position, playerData.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
            yield return new WaitForSeconds(AttackSpeed);
            playerData.weaponPrefab.SetActive(true);
            yield return transform.DOMove(playerData.Base.transform.position, playerData.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
            IsAttack = false;
            playerData.weaponPrefab.SetActive(false);

        }
        
        

    }

}
