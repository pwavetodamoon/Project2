using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Quest 1", menuName = "ScriptableObjects/Quest_SO", order = 1)]
public class QuestSO : ScriptableObject
{
    [SerializeField] private int index = 0;
    [SerializeField] private List<ItemSO> itemsList = new List<ItemSO>();
    /// <summary>
    /// Get index of quest is running
    /// </summary>
    /// <returns></returns>
    public int GetIndexQuest()
    {
        if (index < 0)
        {
            index = 0;
        }
        else if (index >= itemsList.Count)
        {
            index = itemsList.Count - 1;
        }
        return index;
    }
    /// <summary>
    /// Return ItemSO data of current quest
    /// </summary>
    /// <returns></returns>
    public ItemSO GetCurrentItemSO()
    {
        return itemsList[index];
    }
    public List<ItemSO> GetItemSOList()
    {
        return itemsList;
    }
    /// <summary>
    /// Complete quest and move to next quest
    /// </summary>
    public void CompleteQuestItem()
    {
        index++;
        if (index >= itemsList.Count)
        {
            index = 0;
        }
    }
}
