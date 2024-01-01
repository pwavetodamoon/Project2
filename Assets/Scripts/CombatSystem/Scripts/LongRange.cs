using System.Collections;
using UnityEngine;

public class LongRange : AttackBase, IAttack
{
    public void Attack()
    {
        
        //Debug.Log(enemyData);
        
        StartCoroutine(FarAttack());
        IEnumerator FarAttack()
        {
            yield return Instantiate(playerData.weaponPrefab, playerData.Base.position, Quaternion.identity);
        }
    }
}
