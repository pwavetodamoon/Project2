using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Character_BodySprites : MonoBehaviour
{
    [ShowInInspector]
    private List<SpritePart> SpritePartList = new List<SpritePart>();
    private struct SpritePart
    {
        public SpritePart(SpritePartBody spritePart, SpriteRenderer _spriteRenderer)
        {
            part = spritePart;
            spriteRenderer = _spriteRenderer;
        }
        public SpritePartBody part;
        public SpriteRenderer spriteRenderer;
    }

    public enum SpritePartBody
    {
        null_part,
        head,
        body,
        left_arm,
        right_arm,
        left_hand,
        right_hand,
        left_leg,
        right_leg,
        item_sword,
        item_shield,
    }
    private void ResetAllPositionTransformPart()
    {

    }
    [Button(ButtonSizes.Medium)]
    private void LoadNewSpritePartList()
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
            var _spritePartName = GetSpriteBodyPartByName(_spritePart.name);

            var newSpritePart = new SpritePart(_spritePartName, _spritePart);

            SpritePartList.Add(newSpritePart);
        }
        Debug.Log("SpritePartDict loaded");
    }
    /// <summary>
    /// This method is used to change the sprite of a body part 
    /// </summary>
    /// <param name="spritePartName">Which body part you want to change</param>
    /// <param name="newSprite">Sprite you want to set</param>
    /// <returns>return boolen to know sprite is set yet</returns>
    [Button(ButtonSizes.Medium)]
    public bool SetSpriteBodyPart(SpritePartBody spritePartName, Sprite newSprite)
    {
        foreach (var _spritePart in SpritePartList)
        {
            if (_spritePart.part.Equals(spritePartName))
            {
                Debug.Log("Sprite Part found");
                _spritePart.spriteRenderer.sprite = newSprite;
                return true;
            }
        }
        return false;
    }
    private SpritePartBody GetSpriteBodyPartByName(string name)
    {
        switch (name)
        {
            case "s_head":
                return SpritePartBody.head;
            case "s_body":
                return SpritePartBody.body;
            case "s_left_arm":
                return SpritePartBody.left_arm;
            case "s_right_arm":
                return SpritePartBody.right_arm;
            case "s_left_hand":
                return SpritePartBody.left_hand;
            case "s_right_hand":
                return SpritePartBody.right_hand;
            case "s_left_leg":
                return SpritePartBody.left_leg;
            case "s_right_leg":
                return SpritePartBody.right_leg;
            case "s_item_sword":
                return SpritePartBody.item_sword;
            case "s_item_shield":
                return SpritePartBody.item_shield;
            default:
                return SpritePartBody.null_part;
        }
    }
}