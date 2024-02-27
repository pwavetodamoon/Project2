using System.Collections.Generic;
using Helper;
using ObjectPool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DropItem
{
    public class RewardManager : Singleton<RewardManager>
    {
        public ItemDrop itemCollectPrefab;
        public Coin itemCoinPrefab;
        [Header("Item Information")]
        [SerializeField] ItemsSO sliverCoinSO;
        [SerializeField] ItemsSO goldCoinSO;
        [SerializeField] ItemsSO ItemsSO;

        [Header("Drop Setting")]
        [SerializeField] bool enableSpawnCoin = true;
        [SerializeField] bool enableSpawnItem = true;

        ObjectPoolPrefab<Items> itemPool;
        ObjectPoolPrefab<Items> coinPool;
        protected override void Awake()
        {
            base.Awake();
            itemPool = new ObjectPoolPrefab<Items>(itemCollectPrefab, transform, 20);
            coinPool = new ObjectPoolPrefab<Items>(itemCoinPrefab, transform, 20);
            list = new List<Items>();
        }
        [TableList(ShowIndexLabels = true)]
        [ShowInInspector] List<Items> list;
        private void Start()
        {
            QuestManager.Instance.OnChangedQuestItem += UpdateQuestItem;
        }
        private void UpdateQuestItem(ItemsSO newQuestItem)
        {
            ItemsSO = newQuestItem;
        }
        [Button]
        public void CreateReward(Vector3 position)
        {
            var count = Random.Range(1, 5);
            for (int i = 0; i < count; i++)
            {
                if (enableSpawnCoin == false) continue;
                GetItemInPool(sliverCoinSO, coinPool, position);
                
            }

            if (enableSpawnItem == false) return;

            if (QuestManager.Instance.CanSpawnQuestItem == false) return;
            QuestManager.Instance.QuestItemInScene++;
            GetItemInPool(ItemsSO, itemPool, position);
        }

        private void Add(Items item)
        {
            if (list.Contains(item)) return;
            list.Add(item);
        }
        private Items GetItemInPool(ItemsSO item, ObjectPoolPrefab<Items> rewardItemPool, Vector3 spawnPosition)
        {
            var itemReward = rewardItemPool.Get();
            var jumpPosition = spawnPosition + RandomPosition();

            itemReward.itemID = item.Name;
            itemReward.SetSprite(item.Sprite);
            itemReward.transform.position = spawnPosition;
            itemReward.Pump(jumpPosition);
            Add(itemReward);
            return itemReward;
        }
        private Vector3 RandomPosition()
        {
            return new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
        }

    }
}
