﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace Characters
{
    public class Character_Body_Sprites : MonoBehaviour
    {
        public SpriteRenderer head;
        public SpriteRenderer eye;
        public SpriteRenderer body;
        public SpriteRenderer left_arm;
        public SpriteRenderer right_arm;
        public SpriteRenderer left_hand;
        public SpriteRenderer right_hand;
        public SpriteRenderer left_leg;
        public SpriteRenderer right_leg;
        public SpriteRenderer item_sword;
        public SpriteRenderer item_shield;



        public enum SpritePartEnum
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
        [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
        private void LoadSpritePart()
        {
            var allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (var spriteRenderer in allSpriteRenderers)
            {
                SetSingleSpritePart(GetSpritePartEnumByName(spriteRenderer.name), spriteRenderer);
            }
        }
        private void SetSingleSpritePart(SpritePartEnum spritePartEnum, SpriteRenderer spriteRenderer)
        {
            switch (spritePartEnum)
            {
                case SpritePartEnum.head:
                    head = spriteRenderer;
                    break;

                case SpritePartEnum.eye:
                    eye = spriteRenderer;
                    break;

                case SpritePartEnum.body:
                    body = spriteRenderer;
                    break;

                case SpritePartEnum.left_arm:
                    left_arm = spriteRenderer;
                    break;

                case SpritePartEnum.right_arm:
                    right_arm = spriteRenderer;
                    break;

                case SpritePartEnum.left_hand:
                    left_hand = spriteRenderer;
                    break;

                case SpritePartEnum.right_hand:
                    right_hand = spriteRenderer;
                    break;

                case SpritePartEnum.left_leg:
                    left_leg = spriteRenderer;
                    break;

                case SpritePartEnum.right_leg:
                    right_leg = spriteRenderer;
                    break;

                case SpritePartEnum.item_sword:
                    item_sword = spriteRenderer;
                    break;

                case SpritePartEnum.item_shield:
                    item_shield = spriteRenderer;
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
                "s_eye" => SpritePartEnum.eye,
                "s_body" => SpritePartEnum.body,
                "s_left_arm" => SpritePartEnum.left_arm,
                "s_right_arm" => SpritePartEnum.right_arm,
                "s_left_hand" => SpritePartEnum.left_hand,
                "s_right_hand" => SpritePartEnum.right_hand,
                "s_left_leg" => SpritePartEnum.left_leg,
                "s_right_leg" => SpritePartEnum.right_leg,
                "s_item_sword" => SpritePartEnum.item_sword,
                "s_item_shield" => SpritePartEnum.item_shield,
                _ => new SpritePartEnum(),
            };
        }
    }
}