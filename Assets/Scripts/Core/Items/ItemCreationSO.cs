using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Item-Creator", menuName = "ScriptableObjects/Item-Creator-SO", order = 1)]
public class ItemCreationSO : ScriptableObject
{
    // THIS CLASS JUST USE FOR CREATE ITEM SO AUTOMATICALLY
    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    void LoadList()
    {

    }

    [Button(ButtonSizes.Large)]
    void CreateItemBase()
    {
        foreach (var item in sprites)
        {
            string filePath = "Assets/Scripts/Core/Items/ItemSO/" + "ItemSO_" + item.name + ".asset";

            if (File.Exists(filePath))
            {
                Debug.Log("File already exist");
                continue;
            }

            var itemBase = ScriptableObject.CreateInstance<ItemSO>();
            itemBase.Sprite = item;
            itemBase.Id = item.name; //  item name of the sprite is a number so we can use it as a name
            UnityEditor.AssetDatabase.CreateAsset(itemBase, filePath);
        }
    }
}
