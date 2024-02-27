using System;
using Helper;
using UnityEngine;

namespace DropItem
{
    public class QuestManager : Singleton<QuestManager>
    {
        [SerializeField] private ItemsSO questItem;
        [SerializeField] private string idItemNeedCollect => questItem.Name;
        [Header("Quest Information")]
        [SerializeField] private int questItemInScene = 0;
        [SerializeField] private int itemNeedCollect = 5;
        [SerializeField] private int maxItemNeedCollect = 5;
        public int QuestItemInScene { get { return questItemInScene; } set { questItemInScene = value; } }

        public bool CanSpawnQuestItem => questItemInScene < maxItemNeedCollect;

        public Action<ItemsSO> OnChangedQuestItem; // Can use for UI and logic game

        private void OnDisable()
        {
            OnChangedQuestItem = null;
        }

        public void ChangeQuestItem(ItemsSO newItemQuest)
        {
            if (newItemQuest == null) return;
            questItem = newItemQuest;
            itemNeedCollect = maxItemNeedCollect;
            questItemInScene = 0;
            OnChangedQuestItem?.Invoke(questItem);
        }

        public void OnCollectItem(string itemId)
        {
            if (questItem == null) return;
            if (itemId != idItemNeedCollect) return;

            Debug.Log("We collect: " + itemId);
            Debug.Log("Item need collect is: " + (itemNeedCollect - 1));
            itemNeedCollect--;

            if (itemNeedCollect == 0)
            {
                Debug.Log("We fine the quest");
            }
        }
    }
}
