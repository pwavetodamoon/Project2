using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Quest 1", menuName = "ScriptableObjects/Quest_SO", order = 1)]
public class QuestSO : ScriptableObject
{
    public List<ItemSO> itemsList = new List<ItemSO>();
}
