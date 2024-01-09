using System.Collections;

public class LongRange : AttackBase, IAttack
{
    public void Attack()
    {
        //Debug.Log(enemyData);

        StartCoroutine(FarAttack());

        IEnumerator FarAttack()
        {
            //yield return Instantiate(data.weaponPrefab, Transform.position, Quaternion.identity);
            yield return null;
        }
    }
}