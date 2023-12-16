using Sirenix.OdinInspector;
using UnityEngine;

public class Monster_Body_Sprites : MonoBehaviour
{
    public SpriteRenderer head;
    public SpriteRenderer open_eye;
    public SpriteRenderer close_eye;
    public SpriteRenderer left_Wing;
    public SpriteRenderer right_Wing;
    public enum SpritePartEnum
    {
        head,
        open_eye,
        close_eye,
        left_Wing,
        right_Wing,
    }
    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
    void LoadSpritePart()
    {
        var allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriteRenderer in allSpriteRenderers)
        {
            SetSingleSpritePart(GetSpritePartEnumByName(spriteRenderer.name), spriteRenderer);
        }
    }
    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
    public void SetSprite(Sprite _head, Sprite eye1, Sprite eye2, Sprite left, Sprite right)
    {
        head.sprite = _head;
        open_eye.sprite = eye1;
        close_eye.sprite = eye2;
        left_Wing.sprite = left;
        right_Wing.sprite = right;
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
                left_Wing = sprite;
                break;
            case SpritePartEnum.right_Wing:
                right_Wing = sprite;
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
            "s_left_wing" => SpritePartEnum.left_Wing,
            "s_right_wing" => SpritePartEnum.right_Wing,
            _ => new SpritePartEnum(),
        };
    }
}
