using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharactersSpawner : MonoBehaviour
{
    public List<BaseData> CharactersData;
    public CombatManager combatManager;

    public GameObject CharacterPrefab;

    [Button]
    public void CreateCharacters()
    {
        foreach (var data in CharactersData)
        {
            var player = Instantiate(CharacterPrefab, transform);
            player.SetActive(true);
            player.GetComponent<CharactersBase>().data = data;
            AddToList(player);
        }
    }

    private void AddToList(GameObject player)
    {
        if (combatManager == null)
        {
            combatManager = FindObjectOfType<CombatManager>();
        }
        combatManager.Add(player.GetComponent<CharactersBase>());
    }
}