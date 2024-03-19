using System;
using CombatSystem.HeroDataManager.Data;
using Helper;

namespace PlayFab_System
{
    public class PlayerData : Singleton<PlayerData>
    {
        // nguoi choi
        public string customId;
        public string playerName;
        public string email ;
        public string passWord ;
        public int levelPlayer;
        public int gold;
    }
}