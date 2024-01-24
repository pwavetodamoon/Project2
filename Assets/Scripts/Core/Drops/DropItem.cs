using System.Collections.Generic;
using Core.Items;
using Core.Items.InScreen;
using Core.Quest;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Drops
{
    public class DropItem : MonoBehaviour
    {
        [SerializeField] private List<ItemSO> itemsList = new List<ItemSO>();
        [SerializeField] private ItemSO ItemSO_money;
        [SerializeField] private QuestSO questSO;
        [SerializeField] private GameObject item_loot_Prefab;
        [SerializeField] private GameObject item_money_Prefab;
        //public List<ItemBase> collectBaseList;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartRandomDrop();
            }
        }
        [Button(ButtonSizes.Medium)]
        private Vector2 basePosition;
        void StartRandomDrop()
        {
            basePosition = transform.position;

            // Random itemMoney from list
            itemsList = questSO.GetItemSOList();
            var r_itemIndex = Random.Range(0, itemsList.Count);
            var r_itemSO = itemsList[r_itemIndex];

            int r_dropCount = Random.Range(1, 5);

            for (int i = 0; i < r_dropCount; i++)
            {
                var itemMoney = SpawnItem(item_money_Prefab,ItemSO_money, basePosition + R_Position());

                //collectBaseList.Add(itemMoney.GetComponent<ItemBase>());
            }

            var itemLoot = SpawnItem(item_loot_Prefab,r_itemSO, basePosition + R_Position());

            //collectBaseList.Add(itemLoot.GetComponent<ItemBase>());
        }
        [Button]
        private Vector2 R_Position()
        {
            float r_x = Random.Range(-1f, 2f);
            float r_y = Random.Range(-1f, 2f);
            return new Vector2(r_x, r_y);
        }
        private GameObject SpawnItem(GameObject prefab,ItemSO itemSO, Vector2 position)
        {
            var go = Instantiate(prefab, position, Quaternion.identity);
            go.SetActive(true);
            go.GetComponent<SpriteRenderer>().sprite = itemSO.Sprite;
            go.GetComponent<ItemBase>().Id = itemSO.Id;

            return go;
        }
    }
}
