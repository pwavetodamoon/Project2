using System.Collections.Generic;
using Characters;
using CombatSystem;
using NewCombat.Characters;
using NewCombat.HeroDataManager;
using NewCombat.Slots;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using System;
using PlayFab.PfEditor.Json;
using UnityEngine.Serialization;
using NewCombat.HeroDataManager.Data;

public class testfunc : MonoBehaviour
{

    public HeroManager heroManager;
    public UIAvatarController[] uiAvatarControllers;

    private void Start()
    {
        Test();
        Spawn();
    }

    [Button]
    void Test()
    {
        uiAvatarControllers = FindObjectsOfType<UIAvatarController>();
    }

    [Button]
    public void Spawn()
    {
        // Cần phải chạy hàm Test trước để lấy danh sách UI avatar
        // Lấy data list hero data trong hero manager
        var list = heroManager.heroData;
        uiAvatarControllers = uiAvatarControllers.OrderBy(x => x.index).ToArray();
        for (int i = 0; i < list.Count; i++)
        {
            // ref data thứ i từ list hero data
            var heroData = list[i];
            if (heroData.heroCharacter != null) continue;

            // tạo hero từ prefab hero trong hero manager
            var hero = Instantiate(heroManager.prefabHero, CombatEntitiesManager.Instance.transform).GetComponent<HeroCharacter>();
            hero.SetAttackFactory(heroData.GetHeroFactory());

            // chỉnh sprite cho hero
            uiAvatarControllers[i].SetSprite(heroData.icon);

            // Chỉnh data cho class stats của hero
            hero.SetHeroData(heroData);

            // Chỉnh slot index cho hero bởi hero vừa tạo ra chỉ có xác 
            hero.SetSlotIndex(heroData.SlotIndex);

            // Ref hero object vào trong hero data
            heroData.heroCharacter = hero;

            // Chỉnh icon cho avatar
            uiAvatarControllers[i].SetHeroCharacter(hero);

            var heroSkin = hero.GetComponentInChildren<Character_Body_Sprites>();
            heroSkin.SetHeroSprite(heroData.GetSkinDictionary());

            SlotManager.Instance.LoadHeroIntoSlot(hero);

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

    public HeroSaveList heroSaveList = new HeroSaveList();
    [Button]
    private void Test222()
    {
        var list = heroManager.heroData;
        foreach (var heroData in list)
        {
            var heroSave = new HeroSaveData();
            heroSave.Load(heroData);
            heroSaveList.Datas.Add(heroSave);
        }
    }

    [Button]
    public string ConvertTesT()
    {
        string json = JsonUtility.ToJson(heroSaveList, true);
        Debug.Log(json);
        return json;
    }

    public void ConvertBack(string json)
    {
        heroSaveList = JsonUtility.FromJson<HeroSaveList>(json);
    }

}

[Serializable]
public class HeroSaveList
{
    public List<HeroSaveData> Datas = new List<HeroSaveData>();
}
[System.Serializable]
public class HeroSaveData
{
    public string heroName;
    public int slotIndex;
    public StructStats structStats;

    public void Load(HeroData heroData)
    {
        heroData.LoadFromHeroInGame();
        heroName = heroData.HeroName;
        slotIndex = heroData.SlotIndex;
        structStats = heroData.structStats;
    }
}