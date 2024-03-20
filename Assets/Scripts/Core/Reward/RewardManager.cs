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
        public ItemDrop baseCollectPrefab;

        public Coin itemCoinPrefab;

        [Header("Item Information")]
        [SerializeField]
        private ItemsSO sliverCoinSO;

        [SerializeField] private ItemsSO goldCoinSO;


        [Header("Drop Setting")]
        [SerializeField]
        private bool enableDropForMoney = true;

        [SerializeField] private bool enableLootForItem = true;
        private ObjectPoolPrefab<BaseDrop> coinPool;

        private ObjectPoolPrefab<BaseDrop> itemPool;
        [SerializeField] private StageInformation stageInformation;
        [TableList(ShowIndexLabels = true)]
        [ShowInInspector]
        private List<BaseDrop> list;

        private Vector3 RandomPosition() => new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));

        private void OnValidate()
        {
            if (stageInformation == null)
            {
                stageInformation = GetDataSupport.Get().StageInformation;
            }
        }
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
            SpawnMultipleCoin(position);

            SpawnItem(position);
        }
        private void SpawnItem(Vector3 position)
        {
            if (enableLootForItem)
            {
                var itemData = stageInformation.GetRandomItemDrop();
                var itemInGame = GetItemInPool(itemData.GetItemSO(), itemPool, position);
                itemInGame.point = itemData.GetPoint();
            }
        }
        private void SpawnMultipleCoin(Vector3 position)
        {
            var count = Random.Range(1, 5);
            for (var i = 0; i < count; i++)
            {
                if (enableDropForMoney == false) continue;
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

    }
}