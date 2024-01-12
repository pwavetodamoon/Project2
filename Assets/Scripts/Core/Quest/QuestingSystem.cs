using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class QuestingSystem : SerializedMonoBehaviour
{
    public static QuestingSystem Instance;
    [SerializeField] Dictionary<string, int> questDictionary;

    public QuestSO questSO;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        CreateQuest();
    }
    void CreateQuest()
    {
        questDictionary = new Dictionary<string, int>();
        foreach (var item in questSO.GetItemSOList())
        {
            questDictionary.Add(item.Id, 0);
        }
    }

    public void CollectItem(string id)
    {
        if (!questDictionary.ContainsKey(id)) return;
        if (questDictionary[id] > 5) return;

        questDictionary[id]++;
        if (questDictionary[id] == 5)
        {
            Debug.Log("Complete Quest Gather Item: " + id);
            questSO.CompleteQuestItem();
        }
    }
}
