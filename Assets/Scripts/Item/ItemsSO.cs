using Sirenix.OdinInspector;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "Item_", menuName = "ScriptableObjects/ItemsData")]
    public class ItemsSO : ScriptableObject
    {
        public string Name;
        public string Description;

        [AssetList(Path = "Sprites/Loot - Items/Loot Items/shadow")] [PreviewField(70, ObjectFieldAlignment.Center)]
        public Sprite Sprite;
    }
}