using System.Collections;
using UnityEngine;

public class LongRange : AttackBase, IAttack
{
    public LongRange(BaseData data) : base(data)
    {
    }

    public void Attack()
    {
        
        //Debug.Log(enemyData);
        
        StartCoroutine(FarAttack());

        IEnumerator FarAttack()
        {
            //yield return Instantiate(data.weaponPrefab, transform.position, Quaternion.identity);
            yield return null;
        }
    }
}
