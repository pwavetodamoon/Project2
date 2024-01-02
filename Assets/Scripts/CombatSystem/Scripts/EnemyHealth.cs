using UnityEngine;

public class EnemyHealth : HealthBase
{
    private void Start()
    {
        enemy = Game_DataBase.Instance.GetEnemyData(EnemyScript.Instance.ID);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            TakeDamage();
            Debug.Log(currentHealth);
        }
        if (currentHealth <= minHealth)
        {
            Dead(enemy.animationTime);
        }
        
    }
}
