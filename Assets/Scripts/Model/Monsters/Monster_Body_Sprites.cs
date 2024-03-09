using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.Monsters
{
    public class Monster_Body_Sprites : MonoBehaviour
    {
        public enum MonsterType
        {
            Land,
            Flying
        }

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
            left_leg
        }

        public SpriteRenderer head;
        public SpriteRenderer open_eye;
        public SpriteRenderer close_eye;
        public SpriteRenderer left_wing;
        public SpriteRenderer right_wing;
        public SpriteRenderer left_hand;
        public SpriteRenderer right_hand;
        public SpriteRenderer left_leg;
        public SpriteRenderer right_leg;

        public MonsterType monsterType;

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
        public void SetSpriteLand(Sprite _head, Sprite eye1, Sprite eye2, Sprite leftHand, Sprite rightHand,
            Sprite leftLeg, Sprite rightLeg)
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
            }
        }

        [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
        private void LoadSpritePart()
        {
            var allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (var spriteRenderer in allSpriteRenderers)
                SetSingleSpritePart(GetSpritePartEnumByName(spriteRenderer.name), spriteRenderer);
        }

        private SpritePartEnum GetSpritePartEnumByName(string name)
        {
            return name switch
            {
                "head" => SpritePartEnum.head,
                "open_eye" => SpritePartEnum.open_eye,
                "close_eye" => SpritePartEnum.close_eye,
                "left_Wing" => SpritePartEnum.left_Wing,
                "right_Wing" => SpritePartEnum.right_Wing,
                "left_hand" => SpritePartEnum.left_hand,
                "right_hand" => SpritePartEnum.right_hand,
                "left_leg" => SpritePartEnum.left_leg,
                "right_leg" => SpritePartEnum.right_leg,
                _ => SpritePartEnum.head
            };
        }
    }
}