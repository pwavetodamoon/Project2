using System.Collections.Generic;
using System.IO;
using Characters;
using Leveling_System;
using NewCombat.AttackFactory;
using NewCombat.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.HeroDataManager.Data
{
    [CreateAssetMenu(fileName = "Hero", menuName = "CombatSystem/Hero", order = 1)]
    public class HeroData : SerializedScriptableObject
    {
        public StructStats structStats;
        public Sprite icon;
        [ShowInInspector] private Dictionary<Character_Body_Sprites.SpritePartEnum,Sprite> spriteDictionary;
        [SerializeField] protected string heroName;
        [SerializeField] protected AttackTypeEnum AttackType;
        [SerializeField] protected CharacterEnumType characterEnumType;
        [SerializeField] protected BaseStat baseStats;
        [SerializeField] protected int slotIndex = 0;
        [SerializeField] private HeroSingleAttackFactory HeroSingleAttackFactory;

        public HeroCharacter heroCharacter;

        private GameObject heroObject;
        public int SlotIndex
        {
            get => slotIndex;
            set => slotIndex = value;
        }

        public HeroSingleAttackFactory GetHeroFactory()
        {
            return HeroSingleAttackFactory;
        }

        [FolderPath(ParentFolder = "Assets/Resources")]
        public string ResourcePath;

        public Dictionary<Character_Body_Sprites.SpritePartEnum,Sprite> GetSkinDictionary()
        {
            if (spriteDictionary == null)
            {
                LoadAllSkinInFolder();
            }
            return spriteDictionary;
        }

        [Button]
        private void LoadAllSkinInFolder()
        {
            var filePath = Application.dataPath +"/"+ "Resources/" + ResourcePath;
            Debug.Log(filePath);
            Debug.Log(Directory.Exists(filePath));
            var files = filePath;

            var sprites = Resources.LoadAll<Sprite>(ResourcePath);
 
            LoadSpriteHelp.LoadSpritePart(sprites, out spriteDictionary);
            Debug.Log("Load all skin done");
        }
        public enum AttackTypeEnum
        {
            Near,
            Far
        }
    }
}