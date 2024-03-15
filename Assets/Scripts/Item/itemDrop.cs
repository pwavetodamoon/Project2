using Core.Quest;

namespace Item
{
    public class ItemDrop : BaseDrop
    {
        // Update Data in QuestManager
        public override void SendData()
        {
            QuestManager.Instance.OnIncreasePoint(point);
        }
    }
}