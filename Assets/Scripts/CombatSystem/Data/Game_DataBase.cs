using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_DataBase : MonoBehaviour
{
    public static Game_DataBase Instance;
    [SerializeField] private List<CharactersData> playerDatas;
    [SerializeField] private List<EnemyData> enemyDatas;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public CharactersData GetPlayerData(string ID)
    {
        return playerDatas.Find(data => data.Id == ID);
    }
    public EnemyData GetEnemyData(string ID)
    {
        return enemyDatas.Find(data => data.Id == ID);
    }

}
