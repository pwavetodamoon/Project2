using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Character_BodySprites : MonoBehaviour
{
    [SerializeField]
    private List<SpritePart> SpritePartList = new List<SpritePart>();

    private struct SpritePart
    {
        public SpritePart(string _name, SpriteRenderer _spriteRenderer)
        {
            name = _name;
            spriteRenderer = _spriteRenderer;
        }

        public string name;
        public SpriteRenderer spriteRenderer;
    }

    public enum SpritePartName
    {
        head,
        body,
        left_arm,
        right_arm,
        left_hand,
        right_hand,
        left_leg,
        right_leg
    }

    private string spriteHeadName = "s_head";
    private string spriteBodyName = "s_body";
    private string spriteLeftArmName = "s_left_arm";
    private string spriteRightArmName = "s_right_arm";
    private string spriteLeftHandName = "s_left_hand";
    private string spriteRightHandName = "s_right_hand";
    private string spriteLeftLegName = "s_left_leg";
    private string spriteRightLegName = "s_right_leg";

    [Button(ButtonSizes.Medium)]
    private void LoadNewSpritePartDict()
    {
        if (SpritePartList.Count > 0)
        {
            SpritePartList.Clear();
        }
        var spriteRendererList = GetComponentsInChildren<SpriteRenderer>();
        if (spriteRendererList.Length == 0)
        {
            Debug.LogError("No SpriteRenderer found in children");
            return;
        }
        foreach (var _spritePart in spriteRendererList)
        {
            var newSpritePart = new SpritePart(_spritePart.name, _spritePart);
            SpritePartList.Add(newSpritePart);
        }
        Debug.Log("SpritePartDict loaded");
    }

    [Button(ButtonSizes.Medium)]
    private SpriteRenderer GetSprite(SpritePartName spritePartName)
    {
        var partName = GetSpritePartName(spritePartName);
        if (partName == null)
        {
            Debug.LogError("Sprite Part Name not found");
            return null;
        }
        foreach (var _spritePart in SpritePartList)
        {
            if (_spritePart.name == partName)
            {
                Debug.Log("Sprite Part found");
                return _spritePart.spriteRenderer;
            }
        }
        return null;
    }

    private string GetSpritePartName(SpritePartName spritePartName)
    {
        switch (spritePartName)
        {
            case SpritePartName.head:
                return spriteHeadName;

            case SpritePartName.body:
                return spriteBodyName;

            case SpritePartName.left_arm:
                return spriteLeftArmName;

            case SpritePartName.right_arm:
                return spriteRightArmName;

            case SpritePartName.left_hand:
                return spriteLeftHandName;

            case SpritePartName.right_hand:
                return spriteRightHandName;

            case SpritePartName.left_leg:
                return spriteLeftLegName;

            case SpritePartName.right_leg:
                return spriteRightLegName;

            default:
                break;
        }
        return null;
    }
}