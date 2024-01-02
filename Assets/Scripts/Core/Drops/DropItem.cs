using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public List<ItemSO> itemsList = new List<ItemSO>();

    public QuestSO questSO;
    public GameObject itemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDropItemByCurrentQuest());
    }
    IEnumerator StartDropItemByCurrentQuest()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            var itemSO = questSO.GetCurrentItemSO();

            SpawnItem(itemSO, R_Position());
        }
    }
    IEnumerator StartRandomItem()
    {
        // Random item from list
        itemsList = questSO.GetItemSOList();
        while (true)
        {
            yield return new WaitForSeconds(2);
            var r_itemIndex = Random.Range(0, itemsList.Count);
            var r_itemSO = itemsList[r_itemIndex];


            SpawnItem(r_itemSO, R_Position());
        }
    }
    private Vector2 R_Position()
    {
        var r_x = Random.Range(-5, 5);
        var r_y = Random.Range(-5, 5);
        return new Vector2(r_x, r_y);
    }
    private void SpawnItem(ItemSO itemSO, Vector2 position)
    {
        var go = Instantiate(itemPrefab, position, Quaternion.identity);
        go.SetActive(true);
        go.GetComponent<SpriteRenderer>().sprite = itemSO.Sprite;
        go.GetComponent<ItemOnScreen>().Id = itemSO.Id;
    }
}
