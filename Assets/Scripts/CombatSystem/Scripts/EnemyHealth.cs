using UnityEngine;

public class EnemyHealth : HealthBar
{

    private void Start()
    {
        player = Game_DataBase.Instance.GetPlayerData(PlayerScript.Instance.ID);
        enemy = Game_DataBase.Instance.GetEnemyData(EnemyScript.Instance.ID);
        maxHealth = enemy.health;
        currentHealth = maxHealth;
        Damage = player.damage;
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
