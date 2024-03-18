using System;
using Helper;

namespace PlayFab_System
{
    public class PlayerData : Singleton<PlayerData>
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // nguoi choi
        public string customId;
        public string playerName = "UnityGameDev";
        public string email ;
        public string passWord ;
        public int levelPlayer;
        public int gold;
    }
}