using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(collision.TryGetComponent(out EnemyCharacters enemy))
            {
                Debug.Log("Enemy");
                enemy.StartCoroutine(enemy.TimeCount());
            }
        }
    }
}
