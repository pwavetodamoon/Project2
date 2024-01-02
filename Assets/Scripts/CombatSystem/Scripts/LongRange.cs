using System.Collections;
using UnityEngine;

public class LongRange : AttackBase, IAttack
{
    public void Attack(BaseData playerData)
    {
        
        //Debug.Log(enemyData);
        
        StartCoroutine(FarAttack(playerData));

        IEnumerator FarAttack(BaseData playerData)
        {
            //yield return Instantiate(playerData.weaponPrefab, transform.position, Quaternion.identity);
            yield return null;
        }
    }
}
