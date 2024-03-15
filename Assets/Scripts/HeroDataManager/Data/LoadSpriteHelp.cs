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
            "Face 02",
            "Face 03",
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
        public static string[] EyeName =
        {
            "Face 01",
            "Face 02",
            "Face 03",
        };
        public static void LoadSpritePart(Sprite[] sprites,
            out Dictionary<Character_Body_Sprites.SpritePartEnum, Sprite> Dictionary)
        {
            Dictionary = new Dictionary<Character_Body_Sprites.SpritePartEnum, Sprite>();
            for (var i = 0; i < Character_Body_Sprites.SpritePartCount; i++)
                Dictionary.Add((Character_Body_Sprites.SpritePartEnum)i, null);
            foreach (var sprite in sprites)
            {
                //Debug.Log(sprite.name);
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

        internal static void LoadSpriteEye(Sprite[] sprites, out Sprite[] eyeSprites)
        {
            // TODO: Check method
            eyeSprites = new Sprite[3];
            int count = 0;
            for (int i = 0; i < sprites.Length; i++)
            {
                if (count == 3) break;
                if (sprites[i].name == "Face 01")
                {
                    eyeSprites[0] = sprites[i];
                    count++;
                }
                if (sprites[i].name == "Face 02")
                {
                    eyeSprites[1] = sprites[i];
                    count++;
                }
                if (sprites[i].name == "Face 03")
                {
                    eyeSprites[2] = sprites[i];
                    count++;
                }
            }
        }
    }
}