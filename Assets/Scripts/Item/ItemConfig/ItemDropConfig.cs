using UnityEngine;

namespace Item.ItemConfig
{
    [CreateAssetMenu(fileName = "Item_Config_", menuName = "ScriptableObjects/Item Drop Config")]
    public class ItemDropConfig : ScriptableObject
    {
        [Header("Item Drop Config")] public int jumpCount = 1;

        public float jumpForce = 1;
        public float duration = 0.25f;
    }
}