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
        [SerializeField] private int pointCollected = 0;
        [SerializeField] private int pointNeedCollect = 100;
        public event Action<float> OnUpdateQuestProgress;

        public ItemsStruct GetRandomItemQuest()
        {
            var randomIndex = UnityEngine.Random.Range(0, questItem.Count);
            return questItem[randomIndex];
        }

        public void IncreasePointMonsterDead()
        {
            IncreasePoint(5);
        }
        public void OnCollectItem(string itemId, int newPoint)
        {
            if (questItem == null) return;
            //if (itemId != idItemNeedCollect) return;

            Debug.Log("We collect: " + itemId);
            IncreasePoint(newPoint);
        }

        private void IncreasePoint(int point)
        {
            pointCollected += point;
            OnUpdateQuestProgress?.Invoke(GetCompletePercent());

            if (pointCollected >= pointNeedCollect)
            {
                pointCollected = 0;
            }
        }

        private float GetCompletePercent()
        {
            return (float) pointCollected / pointNeedCollect;
        }
    }
}