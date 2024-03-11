using System.Collections.Generic;
using CombatSystem;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using System;
using CombatSystem.Entity;
using CombatSystem.HeroDataManager;
using CombatSystem.HeroDataManager.Data;
using LevelAndStats;
using Model.Hero;
using SlotHero;

public class testfunc : MonoBehaviour
{

    public HeroManager heroManager;
    public UIAvatarController[] uiAvatarControllers;
    public HeroSaveList heroSaveList;


    [Button]
    public void Spawn()
    {
        uiAvatarControllers = FindObjectsOfType<UIAvatarController>();

        // Cần phải chạy hàm Test trước để lấy danh sách UI avatar
        // Lấy data list hero data trong hero manager
        var list = heroManager.heroData;
        uiAvatarControllers = uiAvatarControllers.OrderBy(x => x.index).ToArray();
        for (int i = 0; i < list.Count; i++)
        {
            var heroData = list[i];
            if (heroData.heroCharacter != null) continue;
            var hero = Instantiate(heroManager.prefabHero, CombatEntitiesManager.Instance.transform).GetComponent<HeroCharacter>();
            heroData.heroCharacter = hero;

            hero.SetAttackFactory(heroData.GetHeroFactory());
            hero.IsDead = heroData.isDead;
            hero.SetHeroData(heroData);
            hero.SetSlotIndex(heroData.slotIndex);
            
            var heroSkin = hero.GetComponentInChildren<Character_Body_Sprites>();
            heroSkin.SetHeroSprite(heroData.GetSkinDictionary());

            SlotManager.Instance.LoadHeroIntoSlot(hero);
            if (hero.IsDead)
            {
                hero.SetDeadState();
                hero.ReleaseObject();
            }

            uiAvatarControllers[i].SetSprite(heroData.icon);
            uiAvatarControllers[i].SetHeroCharacter(hero);

        }
        // Load tất cả hero vào vị trí đúng trong game
    }

    [Button]
    public void SaveSlotIndex()
    {
        var list = heroManager.heroData;
        foreach (var heroData in list)
        {
            heroData.OnSaveSlotIndex();
        }
    }

    [Button]
    private void Test222()
    {
        
    }

    [Button]
    public string ConvertToJson()
    {
        var heroSaveList = new HeroSaveList();
        var list = heroManager.heroData;
        foreach (var heroData in list)
        {
            var heroCloudSaveData = new HeroCloudSaveData();
            heroCloudSaveData.LoadFromHeroData(heroData);
            heroSaveList.Datas.Add(heroCloudSaveData);
        }
        string json = JsonUtility.ToJson(heroSaveList, true);
        Debug.Log(json);
        return json;
    }

    public void ConvertJsonBack(string json)
    {
        var heroCloudSaveList = JsonUtility.FromJson<HeroSaveList>(json);
        for (int i = 0; i < heroCloudSaveList.Datas.Count; i++)
        {
            var heroData = heroManager.heroData[i];
            heroData.LoadFromHeroSaveGame(heroCloudSaveList.Datas[i]);
        }
    }

}

[Serializable]
public class HeroSaveList
{
    public List<HeroCloudSaveData> Datas = new List<HeroCloudSaveData>();
}
[System.Serializable]
public class HeroCloudSaveData
{
    public string heroName;
    public int slotIndex;
    public StructStats structStats;
    public bool isDead;

    public void LoadFromHeroData(HeroData heroData)
    {
        heroData.LoadFromHeroInGame();
        heroName = heroData.heroName;
        slotIndex = heroData.slotIndex;
        structStats = heroData.structStats;
        isDead = heroData.isDead;
    }
}