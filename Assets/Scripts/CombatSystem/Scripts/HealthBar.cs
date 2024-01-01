using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Enemy enemy;
    public Player player;
    public float maxHealth;
    public float minHealth;
    public float currentHealth;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        player = Game_DataBase.Instance.GetPlayerData(PlayerScript.Instance.ID);
        enemy = Game_DataBase.Instance.GetEnemyData(EnemyScript.Instance.ID);
        minHealth = 0;

    }

    public void TakeDamage()
    {
        currentHealth -= Damage;
    }
   
    public void Dead(float animationTime)
    {
        Destroy(gameObject, animationTime);
    }
}



public class PlayerHealth : HealthBar
{
    
    private void Start()
    {
        
        maxHealth = player.health;
        Damage = player.damage;
    }
}
