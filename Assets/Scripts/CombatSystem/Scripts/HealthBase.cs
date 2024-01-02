using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Enemy enemy;
    public float maxHealth;
    public float minHealth = 0;
    public float currentHealth;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        minHealth = 0;
        maxHealth = enemy.health;
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth -= Damage;
        if (currentHealth < 0)
        { currentHealth = minHealth; }
    }
   
    public void Dead(float animationTime)
    {
        Destroy(gameObject, animationTime);
    }
}

