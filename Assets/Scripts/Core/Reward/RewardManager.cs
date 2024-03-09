using System.Collections.Generic;
using Core.Quest;
using Helper;
using Item;
using ObjectPool;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Reward
{
    public class RewardManager : Singleton<RewardManager>
    {
        [FormerlySerializedAs("itemBaseCollectPrefab")] [FormerlySerializedAs("itemCollectPrefab")]
        public ItemDrop baseCollectPrefab;

        public Coin itemCoinPrefab;

        [Header("Item Information")] [SerializeField]
        private ItemsSO sliverCoinSO;

        [SerializeField] private ItemsSO goldCoinSO;

        [FormerlySerializedAs("ItemsSO")] [SerializeField]
        private ItemsSO itemStruct;

        [Header("Drop Setting")] [SerializeField]
        private bool enableSpawnCoin = true;

        [SerializeField] private bool enableSpawnItem = true;
        private ObjectPoolPrefab<BaseDrop> coinPool;

        private ObjectPoolPrefab<BaseDrop> itemPool;

        [TableList(ShowIndexLabels = true)] [ShowInInspector]
        private List<BaseDrop> list;

        protected override void Awake()
        {
            base.Awake();
            itemPool = new ObjectPoolPrefab<BaseDrop>(baseCollectPrefab, transform, 20);
            coinPool = new ObjectPoolPrefab<BaseDrop>(itemCoinPrefab, transform, 20);
            list = new List<BaseDrop>();
        }


        [Button]
        public void CreateReward(Vector3 position)
        {
            if (enableSpawnCoin) SpawnMultipleCoin(position);

            if (enableSpawnItem)
            {
                var itemData = QuestManager.Instance.GetRandomItemQuest();
                var itemInGame = GetItemInPool(itemData.itemsSO, itemPool, position);
                itemInGame.point = itemData.pointCollect;
            }
        }

        private void SpawnMultipleCoin(Vector3 position)
        {
            var count = Random.Range(1, 5);
            for (var i = 0; i < count; i++)
            {
                if (enableSpawnCoin == false) continue;
                GetItemInPool(sliverCoinSO, coinPool, position);
            }
        }

        private BaseDrop GetItemInPool(ItemsSO item, ObjectPoolPrefab<BaseDrop> rewardItemPool, Vector3 spawnPosition)
        {
            var itemReward = rewardItemPool.Get();
            var jumpPosition = spawnPosition + RandomPosition();

            itemReward.transform.position = spawnPosition;
            itemReward.itemID = item.Name;
            itemReward.SetSprite(item.Sprite);
            itemReward.Jumping(jumpPosition);

            if (!list.Contains(itemReward)) list.Add(itemReward);

            return itemReward;
        }

        public void CollectAllItemOnActive()
        {
            foreach (var item in list)
            {
                if (item.gameObject.activeSelf == false) continue;
                item.Collect();
            }
        }

        private Vector3 RandomPosition()
        {
            return new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
        }
    }
}