using System.Collections;
using UnityEngine;

public class LongRange : AttackBase
{
    [SerializeField] public Transform shotingPos;

    public void Attack()
    {
        //Debug.Log(EnemyData);
        StopAllCoroutines();
        shotingPos = GetComponent<Transform>();
        IEnumerator FarAttack()
        {
            Debug.Log("Long Range Attack");
            yield return Instantiate(playerData.weaponPrefab, shotingPos.position, Quaternion.identity);
        }
        StartCoroutine(FarAttack());
    }
}
