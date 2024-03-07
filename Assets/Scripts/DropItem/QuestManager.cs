using System;
using System.Collections.Generic;
using Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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

        [Header("Quest Information")]
        [SerializeField] private int pointCollected = 0;
        [SerializeField] private int pointNeedCollect = 100;
        [Header("Stage Information")]
        [SerializeField, Min(1)] private int maxStage;
        [SerializeField, Min(1)] private int currentStage;
        [SerializeField] private int pointNeedPerStage;

        public event Action<float> OnUpdateQuestProgress;
        public event Action OnCompleteOneStage;
        public ItemsStruct GetRandomItemQuest()
        {
            var randomIndex = UnityEngine.Random.Range(0, questItem.Count);
            return questItem[randomIndex];
        }

        public void IncreasePointWhenKillMonster()
        {
            IncreasePoint(5);
        }
        public void OnCollectItemAndIncreaseScore(string itemId, int newPoint)
        {
            if (questItem == null) return;
            //if (itemId != idItemNeedCollect) return;
            //Debug.Log("We collect: " + itemId);
            IncreasePoint(newPoint);
        }

        private void IncreasePoint(int point)
        {
            pointCollected += point;

            if (pointCollected >= pointNeedCollect)
            {
                pointCollected = 0;
            }
            OnUpdateQuestProgress?.Invoke(GetCompletePercent());
            IsOnCompleteStage();
        }

        private void IsOnCompleteStage()
        {
            pointNeedPerStage = pointNeedCollect / maxStage;
            if (pointCollected >= pointNeedPerStage * currentStage)
            {
                OnCompleteOneStage?.Invoke();
                currentStage++;
            }
        }

        private float GetCompletePercent()
        {
            return (float) pointCollected / pointNeedCollect;
        }
    }
}