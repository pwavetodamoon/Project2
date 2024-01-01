using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMoving : MovingGameObj
{
    public Player player;
    public Enemy enemy;
    public float attackRange;
    
    void Start()
    {
        
        enemy = Game_DataBase.Instance.GetEnemyData(EnemyScript.Instance.ID);
        player = Game_DataBase.Instance.GetPlayerData(PlayerScript.Instance.ID);
        target = player.Base;
        foce = enemy.speed;
        
    }
    private void Update()
    {
        float dis = Vector2.Distance(transform.position, player.Base.position);
        Moving();
        if (dis <= attackRange)
        {
            foce = 0;
        }
        else 
        {
            foce = enemy.speed;
        }

    }
}
