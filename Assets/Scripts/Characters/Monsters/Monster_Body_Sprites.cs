using Sirenix.OdinInspector;
using System.Threading;
using UnityEngine;
using UnityEngine.XR;

public class Monster_Body_Sprites : MonoBehaviour
{
    public SpriteRenderer head;
    public SpriteRenderer open_eye;
    public SpriteRenderer close_eye;

    [ShowIf("monsterType", MonsterType.Flying)]
    public SpriteRenderer left_wing;

    [ShowIf("monsterType", MonsterType.Flying)]
    public SpriteRenderer right_wing;

    [ShowIf("monsterType", MonsterType.Land)]
    public SpriteRenderer left_hand;

    [ShowIf("monsterType", MonsterType.Land)]
    public SpriteRenderer right_hand;

    [ShowIf("monsterType", MonsterType.Land)]
    public SpriteRenderer left_leg;

    [ShowIf("monsterType", MonsterType.Land)]
    public SpriteRenderer right_leg;

    public enum MonsterType
    {
        Land,
        Flying,
    }

    public MonsterType monsterType;

    public enum SpritePartEnum
    {
        head,
        open_eye,
        close_eye,
        left_Wing,
        right_Wing,
        left_hand,
        right_hand,
        right_leg,
        left_leg,
    }

    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
    private void LoadSpritePart()
    {
        var allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriteRenderer in allSpriteRenderers)
        {
            SetSingleSpritePart(GetSpritePartEnumByName(spriteRenderer.name), spriteRenderer);
        }
    }

    [ShowIf("monsterType", MonsterType.Flying)]
    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
    public void SetSprite(Sprite _head, Sprite eye1, Sprite eye2, Sprite left, Sprite right)
    {
        SetNormalySprite(_head, eye1, eye2);
        left_wing.sprite = left;
        right_wing.sprite = right;
    }

    [ShowIf("monsterType", MonsterType.Land)]
    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
    public void SetSpriteLand(Sprite _head, Sprite eye1, Sprite eye2, Sprite leftHand, Sprite rightHand, Sprite leftLeg, Sprite rightLeg)
    {
        SetNormalySprite(_head, eye1, eye2);
        left_leg.sprite = leftLeg;
        right_leg.sprite = rightLeg;
        left_hand.sprite = leftHand;
        right_hand.sprite = rightHand;
    }

    private void SetNormalySprite(Sprite _head, Sprite eye1, Sprite eye2)
    {
        head.sprite = _head;
        open_eye.sprite = eye1;
        close_eye.sprite = eye2;
    }

    private void SetSingleSpritePart(SpritePartEnum spritePartEnum, SpriteRenderer sprite)
    {
        switch (spritePartEnum)
        {
            case SpritePartEnum.head:
                head = sprite;
                break;

            case SpritePartEnum.open_eye:
                open_eye = sprite;
                break;

            case SpritePartEnum.close_eye:
                close_eye = sprite;
                break;

            case SpritePartEnum.left_Wing:
                left_wing = sprite;
                break;

            case SpritePartEnum.right_Wing:
                right_wing = sprite;
                break;

            case SpritePartEnum.left_hand:
                left_hand = sprite;
                break;

            case SpritePartEnum.right_hand:
                right_hand = sprite;
                break;

            case SpritePartEnum.left_leg:
                left_leg = sprite;
                break;

            case SpritePartEnum.right_leg:
                right_leg = sprite;
                break;

            default:
                break;
        }
    }

    private SpritePartEnum GetSpritePartEnumByName(string name)
    {
        return name switch
        {
            "s_head" => SpritePartEnum.head,
            "s_open_eye" => SpritePartEnum.open_eye,
            "s_close_eye" => SpritePartEnum.close_eye,
            // Flying Part
            "s_left_wing" => SpritePartEnum.left_Wing,
            "s_right_wing" => SpritePartEnum.right_Wing,
            // Land Part
            "s_left_hand" => SpritePartEnum.left_hand,
            "s_right_hand" => SpritePartEnum.right_hand,
            "s_left_leg" => SpritePartEnum.left_leg,
            "s_right_leg" => SpritePartEnum.right_leg,
            _ => new SpritePartEnum(),
        };
    }
}