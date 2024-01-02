using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    public List<Player> PlayerDatas;

    public GameObject PlayerPrefabs;

    [Button("CreatePlayer")]
    void CreatePlayer()
    {
        foreach (var data in PlayerDatas)
        {
            var player = Instantiate(PlayerPrefabs, transform);
            player.SetActive(true);
            player.GetComponent<PlayerScript>().playerData = data;
        }
    }
}
