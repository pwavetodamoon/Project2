using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemBase", order = 1)]
    public class ItemSO : ScriptableObject
    {
        public string Id;
        [PreviewField(120, ObjectFieldAlignment.Right)]
        public Sprite Sprite;
    }
}
