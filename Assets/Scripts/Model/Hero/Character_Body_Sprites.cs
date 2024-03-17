using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Spriter2UnityDX;
using UnityEngine;

namespace Model.Hero
{
    [Serializable]
    public class HeroSkin
    {
        public Dictionary<Character_Body_Sprites.SpritePartEnum, SpriteRenderer> Dictionary;

        public HeroSkin()
        {
            Dictionary = new Dictionary<Character_Body_Sprites.SpritePartEnum, SpriteRenderer>();
            for (var i = 0; i < Character_Body_Sprites.SpritePartCount; i++)
                Dictionary.Add((Character_Body_Sprites.SpritePartEnum)i, null);
        }

        public void SetSpritePart(Character_Body_Sprites.SpritePartEnum spritePartEnum, SpriteRenderer spriteRenderer)
        {
            Dictionary[spritePartEnum] = spriteRenderer;
        }

        public void Clear()
        {
            Dictionary.Clear();
        }
    }

    public class Character_Body_Sprites : MonoBehaviour
    {
        public enum SpritePartEnum
        {
            none,
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
            item_shield
        }
        public const int SpritePartCount = 12;
        [SerializeField] private Color deadColor;
        [SerializeField] private Color liveColor;
        [SerializeField] private HeroSkin HeroSkin;
        [SerializeField] private TextureController textureController;

        private void OnValidate()
        {
            if (textureController == null)
                textureController = GetComponentInChildren<TextureController>();
        }
        public void SetEyeSprite(Sprite[] sprites)
        {
            textureController.SetSprites(sprites);
        }
        public void SetHeroSprite(Dictionary<SpritePartEnum, Sprite> spriteDictionary)
        {
            LoadSpritePart();
            //Debug.Log("Set Hero Sprite");
            foreach (var skin in HeroSkin.Dictionary)
            {
                if (skin.Value == null) continue;
                skin.Value.sprite = spriteDictionary[skin.Key];
            }
        }

        [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
        private void LoadSpritePart()
        {
            if (HeroSkin == null)
                HeroSkin = new HeroSkin();

            var allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (var spriteRenderer in allSpriteRenderers)
                HeroSkin.SetSpritePart(GetSpritePartEnumByName(spriteRenderer.name), spriteRenderer);
        }

        private SpritePartEnum GetSpritePartEnumByName(string name)
        {
            return name switch
            {
                "Head" => SpritePartEnum.head,
                "Face 01" => SpritePartEnum.eye,
                "Body" => SpritePartEnum.body,
                "Left Arm" => SpritePartEnum.left_arm,
                "Right Arm" => SpritePartEnum.right_arm,
                "Left Hand" => SpritePartEnum.left_hand,
                "Right Hand" => SpritePartEnum.right_hand,
                "Left Leg" => SpritePartEnum.left_leg,
                "Right Leg" => SpritePartEnum.right_leg,
                "Sword" => SpritePartEnum.item_sword,
                "Shield" => SpritePartEnum.item_shield,
                _ => new SpritePartEnum()
            };
        }

        public void SetDeadSprite()
        {
            foreach (var skin in HeroSkin.Dictionary)
            {
                if (skin.Value == null) continue;
                skin.Value.DOColor(deadColor, .25f);
            }
        }

        public void SetRebirthSprite()
        {
            foreach (var skin in HeroSkin.Dictionary)
            {
                if (skin.Value == null) continue;
                skin.Value.DOColor(liveColor, .25f);
            }
        }
    }
}