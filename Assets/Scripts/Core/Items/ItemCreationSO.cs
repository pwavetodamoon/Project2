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
    [SerializeField] List<Sprite> money_item = new List<Sprite>();

    [Button(ButtonSizes.Large)]
    void CreateItemList()
    {
        string filePath = "Assets/Scripts/Core/Items/ItemSO_Loot/";
        CreateList(sprites, filePath);
    }

    [Button(ButtonSizes.Large)]
    void CreateMoneyItemList()
    {
        string filePath = "Assets/Scripts/Core/Items/ItemSO_Money/";
        CreateList(money_item, filePath);
    }
    void CreateList(List<Sprite> list, string filePath)
    {
        foreach (var item in list)
        {
            //string filePath = "Assets/Scripts/Core/Items/ItemSO_Money/" + "ItemSO_" + item.name + ".asset";
            string itemFilePath = filePath + "ItemSO_" + item.name + ".asset";
            if (File.Exists(itemFilePath))
            {
                Debug.Log("File already exist");
                continue;
            }

            var itemBase = ScriptableObject.CreateInstance<ItemSO>();
            itemBase.Sprite = item;
            itemBase.Id = item.name; //  item name of the sprite is a number so we can use it as a name
            UnityEditor.AssetDatabase.CreateAsset(itemBase, itemFilePath);
        }
    }


}
