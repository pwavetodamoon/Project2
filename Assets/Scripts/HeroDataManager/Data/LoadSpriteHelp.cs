using System.Collections.Generic;
using System.Linq;
using Model.Hero;
using UnityEngine;
using UnityEngine.U2D;

namespace CombatSystem.HeroDataManager.Data
{
    public static class LoadSpriteHelp
    {
        public static string[] SpriteName =
        {
            "Body",
            "Face 01",
            "Head",
            "Shield",
            "Weapon",
            "Left Arm",
            "Left Hand",
            "Left Leg",
            "Right Arm",
            "Right Hand",
            "Right Leg"
        };
        public static void LoadSpritePart(Sprite[] sprites,
            out Dictionary<Character_Body_Sprites.SpritePartEnum, Sprite> Dictionary,
            out Sprite[] eyeSprites)
        {
            Dictionary = new Dictionary<Character_Body_Sprites.SpritePartEnum, Sprite>();
            for (var i = 0; i < Character_Body_Sprites.SpritePartCount; i++)
            {
                Dictionary.Add((Character_Body_Sprites.SpritePartEnum)i, null);

            }

            eyeSprites = new Sprite[3];
            foreach (var sprite in sprites)
            {
                LoadSpriteEye(sprite, ref eyeSprites);
                if (SpriteName.Contains(sprite.name) == false) continue;
                var enumType = GetEnumByFileName(sprite.name);
                Dictionary[enumType] = sprite;
            }
        }

        public static Character_Body_Sprites.SpritePartEnum GetEnumByFileName(string fileName)
        {
            switch (fileName)
            {
                case "Body": return Character_Body_Sprites.SpritePartEnum.body;
                case "Face 01": return Character_Body_Sprites.SpritePartEnum.eye;
                case "Head": return Character_Body_Sprites.SpritePartEnum.head;
                case "Shield": return Character_Body_Sprites.SpritePartEnum.item_shield;
                case "Weapon": return Character_Body_Sprites.SpritePartEnum.item_sword;
                case "Left Arm": return Character_Body_Sprites.SpritePartEnum.left_arm;
                case "Left Hand": return Character_Body_Sprites.SpritePartEnum.left_hand;
                case "Left Leg": return Character_Body_Sprites.SpritePartEnum.left_leg;
                case "Right Arm": return Character_Body_Sprites.SpritePartEnum.right_arm;
                case "Right Hand": return Character_Body_Sprites.SpritePartEnum.right_hand;
                case "Right Leg": return Character_Body_Sprites.SpritePartEnum.right_leg;
            }

            return Character_Body_Sprites.SpritePartEnum.none;
        }

        private static void LoadSpriteEye(Sprite sprite, ref Sprite[] eyeSprites)
        {
            if (sprite.name == "Face 01")
            {
                eyeSprites[0] = sprite;
            }
            if (sprite.name == "Face 02")
            {
                eyeSprites[1] = sprite;
            }
            if (sprite.name == "Face 03")
            {
                eyeSprites[2] = sprite;
            }
        }
    }
}