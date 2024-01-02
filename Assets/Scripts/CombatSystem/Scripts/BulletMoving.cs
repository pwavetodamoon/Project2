using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoving : MovingGameObj
{
    public Enemy enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        rotate = 180;
        enemy = Game_DataBase.Instance.GetEnemyData(EnemyScript.Instance.ID);
        target = enemy.Base;
        Moving();
        foce = 25;
        Destroy(gameObject, 3);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
