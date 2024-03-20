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
    }
}