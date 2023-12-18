using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_DataBase : MonoBehaviour
{
    public static Game_DataBase Instance;
    [SerializeField] private List<PlayerData> playerdatas;
    [SerializeField] private List<Enemy> enemydatas;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public PlayerData GetPlayerData(string ID)
    {
        return playerdatas.Find(data => data.Id == ID);

    }
}
