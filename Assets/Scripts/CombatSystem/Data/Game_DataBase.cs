using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_DataBase : MonoBehaviour
{
    public static Game_DataBase Instance;
    [SerializeField] private List<Player> playerDatas;
    [SerializeField] private List<Enemy> enemyDatas;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public Player GetPlayerData(string ID)
    {
        return playerDatas.Find(data => data.Id == ID);
    }
    public Enemy GetEnemyData(string ID)
    {
        return enemyDatas.Find(data => data.Id == ID);
    }

}
