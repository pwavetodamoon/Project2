using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharactersSpawner : MonoBehaviour
{
    public List<BaseData> PlayerDatas;
    public CombatManager combatManager;

    public GameObject PlayerPrefabs;

    [Button("CreatePlayer")]
    public void CreatePlayer()
    {
        foreach (var data in PlayerDatas)
        {
            var player = Instantiate(PlayerPrefabs, transform);
            player.SetActive(true);
            player.GetComponent<CharactersBase>().data = data;
            AddToList(player);
        }
    }
    void AddToList(GameObject player)
    {
        if(combatManager == null)
        {
            combatManager = FindObjectOfType<CombatManager>();
        }
        combatManager.Add(player.GetComponent<CharactersBase>());

    }
}
