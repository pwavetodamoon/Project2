using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Loot : ItemCollectBase
{
    protected override void Collect()
    {
        base.Collect();
        QuestingSystem.Instance.CollectItem(Id);
        transform.gameObject.SetActive(false);
    }
}
