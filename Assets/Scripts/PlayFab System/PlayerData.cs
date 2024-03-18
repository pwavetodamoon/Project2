using Helper;

namespace PlayFab_System
{
    public class PlayerData : Singleton<PlayerData>
    {
        // nguoi choi
        public string customId;
        public string playerName;
        public string email = "gamedev@gmail.com";
        public string passWord = "123123";
        public int levelPlayer;
        public int gold;
    }
}