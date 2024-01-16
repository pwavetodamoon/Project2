using System.Collections;

namespace CombatSystem.Scripts.Attack
{
    public class LongRange : IAttack
    {
        public void Attack()
        {
            //Debug.Log(enemyData);

            // StartCoroutine(FarAttack());

            IEnumerator FarAttack()
            {
                //yield return Instantiate(data.weaponPrefab, Transform.position, Quaternion.identity);
                yield return null;
            }
        }
    }
}