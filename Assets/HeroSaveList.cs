using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using CombatSystem.HeroDataManager;

[Serializable]
public class HeroSaveList
{
    public HeroManager HeroManager
    {
        get => GetDataSupport.Get().HeroManager;
    }
    public List<HeroCloudSaveData> Datas = new List<HeroCloudSaveData>();
    public string ConvertToJson()
    {
        // heroManager = GetScriptableObjectSupport.Instance.HeroManager;
        Datas = new List<HeroCloudSaveData>();
        var list = HeroManager.heroData;
        foreach (var heroData in list)
        {
            var heroCloudSaveData = new HeroCloudSaveData();
            heroCloudSaveData.LoadFromHeroData(heroData);
            Datas.Add(heroCloudSaveData);
        }
        string json = JsonUtility.ToJson(Datas, true);
        // Debug.Log(json);
        return json;
    }
    [Button]
    public void ConvertJsonBack(string json)
    {
        Debug.Log(json);
        Datas = new List<HeroCloudSaveData>();
        var heroCloudSaveList = JsonUtility.FromJson<HeroSaveList>(json);
        for (int i = 0; i < heroCloudSaveList.Datas.Count; i++)
        {
            if (i >= HeroManager.heroData.Count) break;
            var heroData = HeroManager.heroData[i];
            heroData.LoadFromHeroSaveGame(heroCloudSaveList.Datas[i]);
        }
        Datas = heroCloudSaveList.Datas;
    }

    public void Load(string value)
    {
        ConvertJsonBack(value);
    }

    public string Get()
    {
        return ConvertToJson();
    }
}
