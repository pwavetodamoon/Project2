using System.Collections.Generic;
using CombatSystem.Attack.Factory;
using CombatSystem.Entity;
using LevelAndStats;
using Model.Hero;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem.HeroDataManager.Data
{
    [CreateAssetMenu(fileName = "Hero", menuName = "CombatSystem/Hero", order = 1)]
    public class HeroData : SerializedScriptableObject
    {
        public enum AttackTypeEnum
        {
            Near,
            Far
        }

        public StructStats structStats;
        public Sprite icon;
        [SerializeField] protected string heroName;
        [SerializeField] protected AttackTypeEnum AttackType;
        [SerializeField] protected CharacterEnumType characterEnumType;
        [SerializeField] protected int slotIndex;
        [SerializeField] private HeroSingleAttackFactory HeroSingleAttackFactory;
        public bool isDead;
        public HeroCharacter heroCharacter;

        [FolderPath(ParentFolder = "Assets/Resources")]
        public string ResourcePath;

        private GameObject heroObject;
        [ShowInInspector] private Dictionary<Character_Body_Sprites.SpritePartEnum, Sprite> spriteDictionary;

        public int SlotIndex
        {
            get => slotIndex;
            set => slotIndex = value;
        }

        public string HeroName
        {
            get => heroName;
            set => heroName = value;
        }

        public HeroSingleAttackFactory GetHeroFactory()
        {
            return HeroSingleAttackFactory;
        }

        public void OnSaveSlotIndex()
        {
            slotIndex = heroCharacter.InGameSlotIndex;
        }

        public Dictionary<Character_Body_Sprites.SpritePartEnum, Sprite> GetSkinDictionary()
        {
            if (spriteDictionary == null) LoadAllSkinInFolder();
            return spriteDictionary;
        }

        [Button]
        public void LoadFromHeroInGame()
        {
            // TODO: Add is dead field
            if (heroCharacter == null) return;
            structStats = heroCharacter.GetComponent<HeroEntityStats>().GetStructStats();
            slotIndex = heroCharacter.InGameSlotIndex;
            isDead = heroCharacter.IsDead;
        }

        public void LoadFromHeroSaveGame(HeroCloudSaveData heroCloudSave)
        {
            heroName = heroCloudSave.heroName;
            structStats = heroCloudSave.structStats;
            slotIndex = heroCloudSave.slotIndex;
            isDead = heroCloudSave.isDead;
        }

        [Button]
        private void LoadAllSkinInFolder()
        {
            var filePath = Application.dataPath + "/" + "Resources/" + ResourcePath;
            //Debug.Log(filePath);
            //Debug.Log(Directory.Exists(filePath));
            var files = filePath;

            var sprites = Resources.LoadAll<Sprite>(ResourcePath);

            LoadSpriteHelp.LoadSpritePart(sprites, out spriteDictionary);
            //Debug.Log("Load all skin done");
        }
    }
}