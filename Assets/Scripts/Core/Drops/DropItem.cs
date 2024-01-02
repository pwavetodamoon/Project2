using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private List<ItemSO> itemsList = new List<ItemSO>();
    [SerializeField] private ItemSO ItemSO_money;
    [SerializeField] private QuestSO questSO;
    [SerializeField] private GameObject item_loot_Prefab;
    [SerializeField] private GameObject item_money_Prefab;

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

        // Random item from list
        itemsList = questSO.GetItemSOList();
        var r_itemIndex = Random.Range(0, itemsList.Count);
        var r_itemSO = itemsList[r_itemIndex];

        int r_dropCount = Random.Range(1, 5);

        for (int i = 0; i < r_dropCount; i++)
        {
            SpawnItem(item_money_Prefab,ItemSO_money, basePosition + R_Position());
        }

        SpawnItem(item_loot_Prefab,r_itemSO, basePosition + R_Position());
    }
    [Button]
    private Vector2 R_Position()
    {
        float r_x = Random.Range(-1, 2);
        float r_y = Random.Range(-1, 2);
        return new Vector2(r_x, r_y);
    }
    private void SpawnItem(GameObject prefab,ItemSO itemSO, Vector2 position)
    {
        var go = Instantiate(prefab, position, Quaternion.identity);
        go.SetActive(true);
        go.GetComponent<SpriteRenderer>().sprite = itemSO.Sprite;
        go.GetComponent<ItemCollectBase>().Id = itemSO.Id;
    }
}
