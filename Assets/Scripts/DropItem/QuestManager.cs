using System;
using System.Collections.Generic;
using Helper;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DropItem
{
    [Serializable]
    public struct ItemsStruct
    {
        public int pointCollect;
        public ItemsSO itemsSO;
    }

    public class QuestManager : Singleton<QuestManager>
    {
        public List<ItemsStruct> questItem;

        //[SerializeField] private string idItemNeedCollect => questItem.Name;
        [Header("Quest Information")]
        [SerializeField] private int questItemInScene = 0;

        [SerializeField] private int itemNeedCollect = 5;
        [SerializeField] private int maxItemNeedCollect = 5;
        [SerializeField] private int pointCollected = 0;
        [SerializeField] private int pointNeedCollect = 100;
        public bool CanSpawnQuestItem => true;

        public Action<ItemsSO> OnChangedQuestItem; // Can use for UI and logic game

        private void OnDisable()
        {
            OnChangedQuestItem = null;
        }

        public ItemsStruct GetRandomItemQuest()
        {
            var randomIndex = UnityEngine.Random.Range(0, questItem.Count);
            return questItem[randomIndex];
        }

        public void ChangeQuestItem(List<ItemsStruct> newItemQuest)
        {
            if (newItemQuest == null) return;
            questItem = newItemQuest;
            itemNeedCollect = maxItemNeedCollect;
            questItemInScene = 0;
        }

        public void OnCollectItem(string itemId, int newPoint)
        {
            if (questItem == null) return;
            //if (itemId != idItemNeedCollect) return;

            Debug.Log("We collect: " + itemId);
            Debug.Log("Item need collect is: " + (itemNeedCollect - 1));
            itemNeedCollect--;
            pointCollected += newPoint;
            if (itemNeedCollect == 0)
            {
                Debug.Log("We fine the quest");
            }
        }

        [Button]
        public float GetCompletePercent()
        {
            return (float) pointCollected / pointNeedCollect;
        }
    }
}