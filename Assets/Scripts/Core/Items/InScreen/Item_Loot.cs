using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Loot : ItemBase
{
    public override void Gather()
    {
        base.Gather();
        QuestingSystem.Instance.CollectItem(Id);
    }

}
