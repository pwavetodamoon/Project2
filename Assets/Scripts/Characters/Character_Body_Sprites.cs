using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Character_Body_Sprites : MonoBehaviour
{
    [ShowInInspector]
    public Dictionary<SpritePartBody, SpriteRenderer> keyValuePairs = new Dictionary<SpritePartBody, SpriteRenderer>();

    public enum SpritePartBody
    {
        head,
        eye,
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
    private void Start()
    {
        //transform.DOMove(Vector2.zero, 1f).SetEase(Ease.InOutSine).OnComplete(() =>
        //{
        //    transform.DOMove()
        //});
    }
    private void ResetAllPositionTransformPart()
    {
    }
    /// <summary>
    /// This method is used to change the sprite of a body part 
    /// </summary>
    /// <param name="spritePartName">Which body part you want to change</param>
    /// <param name="newSprite">Sprite you want to set</param>
    /// <returns>return boolen to know sprite is set yet</returns>
    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton, Expanded = true)]
    public bool SetSpriteBodyPart(SpritePartBody spritePartName, Sprite newSprite)
    {
        if (newSprite == null)
        {
            Debug.Log("Set Null Sprite Part: " + spritePartName);
        }
        if (keyValuePairs.ContainsKey(spritePartName))
        {
            keyValuePairs[spritePartName].sprite = newSprite;
            return true;
        }
        return false;
    }
    [Button(ButtonSizes.Medium)]
    private void LoadNewSpritePartList()
    {
        if (keyValuePairs.Count > 0)
        {
            keyValuePairs.Clear();
        }

        var spriteRendererList = GetComponentsInChildren<SpriteRenderer>();
        StringBuilder stringBuilder = new StringBuilder("All part loaded: ");
        foreach (var _spritePart in spriteRendererList)
        {
            var _spritePartName = GetSpriteBodyPartByName(_spritePart.name);

            keyValuePairs.Add(_spritePartName, _spritePart);
            stringBuilder.AppendLine(_spritePart.name + " ");
        }
        Debug.Log(stringBuilder.ToString());
    }

    private SpritePartBody GetSpriteBodyPartByName(string name)
    {
        switch (name)
        {
            case "s_head":
                return SpritePartBody.head;
            case "s_eye":
                return SpritePartBody.eye;
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
                break;
        }
        return new SpritePartBody();
    }
}