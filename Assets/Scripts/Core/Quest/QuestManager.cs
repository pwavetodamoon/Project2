using System;
using System.Collections.Generic;
using Helper;
using Item;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Quest
{
    [Serializable]
    public struct ItemsStruct
    {
        [SerializeField] private int pointCollect;
        [SerializeField] private ItemsSO itemsSO;

        public ItemsSO GetItemSO() => itemsSO;
        public int GetPoint() => pointCollect;
    }

    public class QuestManager : Singleton<QuestManager>
    {
        public StageInformation stageInformation;
        private void OnValidate()
        {
            if (stageInformation == null)
            {
                stageInformation = GetScriptableObjectSupport.Instance.StageInformation;
            }

        }
        public void IncreasePointWhenKillMonster()
        {
            stageInformation.IncreasePointWhenKillMonster();
        }

        public void OnIncreasePoint(int newPoint)
        {
            stageInformation.IncreasePoint(newPoint);
        }

    }
}