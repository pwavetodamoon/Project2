namespace DropItem
{
    public class ItemDrop : BaseDrop
    {
        // Update Data in QuestManager
        public override void SendData()
        {
            QuestManager.Instance.OnCollectItemAndIncreaseScore(itemID, point);
        }
    }
}