using System;
using CombatSystem.HeroDataManager;
using CombatSystem.HeroDataManager.Data;
using Helper;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PlayFab_System
{
    public class PlayerData : Singleton<PlayerData>
    {
        // nguoi choi
        public string customId;
        public string playerName;
        public string email;
        public string passWord;
        public int levelPlayer;
        public int gold;
        private void Start()
        {
            heroManager = GetScriptableObjectSupport.Instance.HeroManager;

        }
        [SerializeField] private HeroManager heroManager;
        public HeroSaveList heroSaveList = new HeroSaveList();
        [Button]
        public string ConvertToJson()
        {
            // heroManager = GetScriptableObjectSupport.Instance.HeroManager;
            heroSaveList = new HeroSaveList();
            var list = heroManager.heroData;
            foreach (var heroData in list)
            {
                var heroCloudSaveData = new HeroCloudSaveData();
                heroCloudSaveData.LoadFromHeroData(heroData);
                heroSaveList.Datas.Add(heroCloudSaveData);
            }
            string json = JsonUtility.ToJson(heroSaveList, true);
            // Debug.Log(json);
            return json;
        }
        [Button]
        public void ConvertJsonBack(string json)
        {
            Debug.Log(json);
            var heroCloudSaveList = JsonUtility.FromJson<HeroSaveList>(json);
            for (int i = 0; i < heroCloudSaveList.Datas.Count; i++)
            {
                if (i >= heroManager.heroData.Count) break;
                var heroData = heroManager.heroData[i];
                heroData.LoadFromHeroSaveGame(heroCloudSaveList.Datas[i]);
            }
            heroSaveList.Datas = heroCloudSaveList.Datas;
        }
        [Button]
        public void Test()
        {
            var json = ConvertToJson();
            ConvertJsonBack(json);
        }
    }
}