namespace DropItem
{
    public class ItemDrop : Items
    {
        // Update Data in QuestManager
        public override void SendData()
        {
            QuestManager.Instance.OnCollectItem(itemID, point);
        }

    }
}
