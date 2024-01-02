using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Money : ItemCollectBase
{
    protected override void Collect()
    {
        base.Collect();
        transform.gameObject.SetActive(false);
    }
}
