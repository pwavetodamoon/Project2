using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.Hero
{
    [Serializable]
    public class HeroSkin
    {
        [ShowInInspector] public Dictionary<Character_Body_Sprites.SpritePartEnum, SpriteRenderer> Dictionary;

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

        public const int SpritePartCount = 11;
        [SerializeField] private Color deadColor;
        [SerializeField] private Color liveColor;
        [SerializeField] private HeroSkin HeroSkin;

        public void SetHeroSprite(Dictionary<SpritePartEnum, Sprite> spriteDictionary)
        {
            LoadSpritePart();
            Debug.Log("Set Hero Sprite");
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