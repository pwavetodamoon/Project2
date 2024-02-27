using UnityEngine;

namespace DropItem
{
    [CreateAssetMenu(fileName = "Item_", menuName = "ScriptableObjects/ItemsData")]
    public class ItemsSO : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Sprite;

    }
}
