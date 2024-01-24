using Core.Quest;

namespace Core.Items.InScreen
{
    public class Item_Loot : ItemBase
    {
        public override void Gather()
        {
            base.Gather();
            QuestingSystem.Instance.CollectItem(Id);
        }

    }
}
