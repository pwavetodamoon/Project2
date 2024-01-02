using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemBase", order = 1)]
public class ItemSO : ScriptableObject
{
    public string Id;
    [PreviewField(120, ObjectFieldAlignment.Right)]
    public Sprite Sprite;
}
