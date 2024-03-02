using System.Collections.Generic;
using Helper;
using ObjectPool;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace DropItem
{
    public class RewardManager : Singleton<RewardManager>
    {
        public ItemDrop itemCollectPrefab;
        public Coin itemCoinPrefab;

        [Header("Item Information")]
        [SerializeField] private ItemsSO sliverCoinSO;

        [SerializeField] private ItemsSO goldCoinSO;
        [FormerlySerializedAs("ItemsSO")][SerializeField] private ItemsSO itemStruct;

        [Header("Drop Setting")]
        [SerializeField] private bool enableSpawnCoin = true;

        [SerializeField] private bool enableSpawnItem = true;

        private ObjectPoolPrefab<Items> itemPool;
        private ObjectPoolPrefab<Items> coinPool;

        protected override void Awake()
        {
            base.Awake();
            itemPool = new ObjectPoolPrefab<Items>(itemCollectPrefab, transform, 20);
            coinPool = new ObjectPoolPrefab<Items>(itemCoinPrefab, transform, 20);
            list = new List<Items>();
        }

        [TableList(ShowIndexLabels = true)]
        [ShowInInspector] private List<Items> list;


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

            var itemStruct = QuestManager.Instance.GetRandomItemQuest();
            var itemInGame = GetItemInPool(itemStruct.itemsSO, itemPool, position);
            itemInGame.point = itemStruct.pointCollect;
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