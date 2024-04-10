using System.Collections.Generic;
using CombatSystem;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using System;
using CombatSystem.Entity;
using CombatSystem.HeroDataManager;
using Model.Hero;
using SlotHero;
using UnityEditor.SceneManagement;

public class Testfunc : MonoBehaviour
{

    public HeroManager heroManager;
    public UIAvatarController[] uiAvatarControllers;
    // public HeroSaveList heroSaveList;
    private void OnValidate()
    {
        if (heroManager == null)
            heroManager = GetDataSupport.Get().HeroManager;
        if (stageInformation == null)
            stageInformation = GetDataSupport.Get().StageInformation;
    }
    public StageInformation stageInformation;
    [Button]
    public void Spawn()
    {
        //Debug.Log("Spawn");
        uiAvatarControllers = FindObjectsOfType<UIAvatarController>();

        // Cần phải chạy hàm Test trước để lấy danh sách UI avatar
        // Lấy data list hero data trong hero manager
        var list = heroManager.heroData;
        uiAvatarControllers = uiAvatarControllers.OrderBy(x => x.index).ToArray();
        for (int i = 0; i < uiAvatarControllers.Length; i++)
        {
            var heroData = list[i];
            if (heroData.heroCharacter != null) continue;
            var hero = Instantiate(heroManager.prefabHero, null).GetComponent<HeroCharacter>();
            heroData.heroCharacter = hero;

            hero.SetAttackFactory(heroData.GetHeroFactory());
            hero.IsDead = heroData.isDead;
            hero.SetHeroData(heroData);
            hero.SetSlotIndex(heroData.slotIndex);

            var heroSkin = hero.GetComponentInChildren<Character_Body_Sprites>();
            heroSkin.SetHeroSprite(heroData.GetSkinDictionary());
            heroSkin.SetEyeSprite(heroData.GetEyeSkin());
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

    // [Button]
    // public string ConvertToJson()
    // {
    //     var heroSaveList = new HeroSaveList();
    //     var list = heroManager.heroData;
    //     foreach (var heroData in list)
    //     {
    //         var heroCloudSaveData = new HeroCloudSaveData();
    //         heroCloudSaveData.LoadFromHeroData(heroData);
    //         heroSaveList.Datas.Add(heroCloudSaveData);
    //     }
    //     string json = JsonUtility.ToJson(heroSaveList, true);
    //     Debug.Log(json);
    //     return json;
    // }

    // public void ConvertJsonBack(string json)
    // {
    //     var heroCloudSaveList = JsonUtility.FromJson<HeroSaveList>(json);
    //     for (int i = 0; i < heroCloudSaveList.Datas.Count; i++)
    //     {
    //         var heroData = heroManager.heroData[i];
    //         heroData.LoadFromHeroSaveGame(heroCloudSaveList.Datas[i]);
    //     }
    // }

}
