using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public float maxHealth;
    public float minHealth = 0;
    public float currentHealth;
    // Start is called before the first frame update
    public void Setup(BaseData enemy)
    {
        minHealth = 0;
        maxHealth = enemy.health;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;
        if (currentHealth < 0)
        { 
            currentHealth = minHealth; 
            Debug.Log("EnemyCharacters is dead");
        }
    }

}

