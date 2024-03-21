using System.Collections;
using System.Collections.Generic;
using System.Text;
using CombatSystem.HeroDataManager.Data;
using Core.Currency;
using Core.Quest;
using deVoid.Utils;
using Helper;
using PlayFab;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using SystemInfo = UnityEngine.Device.SystemInfo;
using LoginResult = PlayFab.ClientModels.LoginResult;
using PlayFabError = PlayFab.PlayFabError;

namespace PlayFab_System
{
    public class PlayFabManager : Singleton<PlayFabManager>
    {
        public PlayerData Player;
        public CurrencyManager currencyManager;
        public Testfunc testfunc;
        public StageInformation stageInformation;

        private static PlayFabManager _instance;

        // Getter cho instance
        private void OnValidate()
        {
            if (stageInformation == null)
                stageInformation = GetScriptableObjectSupport.Instance.StageInformation;
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            Player = PlayerData.Instance;
            Player.customId = SystemInfo.deviceUniqueIdentifier;
            DontDestroyOnLoad(Player);

        }

        private void InitLogin(string email, string pass)
        {
            Login(email, pass);
        }

        public void InitResource()
        {
            Debug.Log("InitResource");
            currencyManager = FindObjectOfType<CurrencyManager>();
            testfunc = FindObjectOfType<Testfunc>();
            // stageInformation = FindObjectOfType<StageInformation>();
            testfunc.Spawn();

        }

        private void OnApplicationQuit()
        {
            SaveDataPlayer();
        }
        public void WaitLogin(string email , string pass)

        {
            StartCoroutine(Wait(email, pass));
        }
        private IEnumerator Wait(string email, string pass)
        {
            yield return new WaitForSeconds(1f);
            InitLogin(email, pass);
            yield return new WaitForSeconds(1f);
            // GameLevelControl.Instance.LoadToCurrentMap();
        }
        [Button]
        public void GetDataPlayer()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnRevcievedData, OnDataSendError);
        }

        [Button]
        public void SaveDataPlayer()
        {
            Debug.Log("save");
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { "CusTomId", Player.customId },
                    {"PlayerName",Player.playerName},
                    { "Email", Player.email },
                    { "Password", Player.passWord },
                    { "Level", Player.levelPlayer.ToString() },
                    { "Gold", Player.gold.ToString() },
                    { "HeroData", Player.heroSaveList.Get() },
            //  { "Hero Data", testfunc.ConvertToJson() },
                }
            };
            Debug.Log("SaveDataPlayer");
            PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnDataSendError);
        }

        [Button]
        public void SaveRewardData()
        {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                    {
                        { "Point", stageInformation.pointCollected.ToString() },
                        { "StageIndex", stageInformation.currentStageIndex.ToString() },
                        { "MapIndex", stageInformation.currentMapIndex.ToString() }
                    }
            }, result => { Debug.Log("Save level data success!"); },
                error => { Debug.LogError("Fail to save data: " + error.ErrorMessage); });
        }
        [Button]
        public void SaveGameLevel()
        {
            Player.gold = currencyManager.currency;
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                    {
                        { "Gold", Player.gold.ToString() }
                    }
            }, result => { Debug.Log("Save data success!"); },
                error => { Debug.LogError("Fail to save data: " + error.ErrorMessage); });
        }
        private void OnRevcievedData(GetUserDataResult result)
        {
            if (result.Data == null || !result.Data.ContainsKey("Email"))
            {
                Debug.Log("No data found");
                return;
            }
            Debug.Log("founded");
            Player.customId = result.Data["CusTomId"].Value;
            Player.playerName = result.Data["PlayerName"].Value;
            Player.email = result.Data["Email"].Value;
            Player.passWord = result.Data["Password"].Value;
            Player.levelPlayer = int.Parse(result.Data["Level"].Value);
            Player.gold = int.Parse(result.Data["Gold"].Value);
            Player.heroSaveList.Load(result.Data["HeroData"].Value);
            
            // testfunc.ConvertJsonBack(result.Data["Hero Data"].Value);
            // stageInformation.currentStageIndex = int.Parse(result.Data["StageIndex"].Value);
            // stageInformation.currentMapIndex = int.Parse(result.Data["MapIndex"].Value);
            // stageInformation.pointCollected = int.Parse(result.Data["Point"].Value);
            DebugResult(result);
        }

        private void DebugResult(GetUserDataResult result)
        {
            var sb = new StringBuilder();
            foreach (var item in result.Data) sb.Append(item.Key).Append(": ").Append(item.Value.Value).Append("\n");
            Debug.Log(sb.ToString());
            // Debug.Log(Player.customId + "email" + Player.email + "passWord" + Player.passWord + "Level" + Player.levelPlayer + "Gold " + Player.gold);
            // Debug.Log(Player.customId + "email" + Player.email + "passWord" + Player.passWord + "Level" + Player.levelPlayer + "Gold " + Player.gold + " " + result.Data["Hero Data"].Value);
        }

        private void OnDataSend(UpdateUserDataResult result)
        {
            Debug.Log("Data sent to PlayFab");
        }

        private void OnDataSendError(PlayFabError error)
        {
            Debug.Log("Error sending data to PlayFab: " + error.GenerateErrorReport());
        }

        #region Login

        public void Login(string email, string pass)
        {
            Debug.Log("Login");
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                Debug.Log("test login is null");
                var message = "Login Fail!";
            Signals.Get<SendMessageLoginRegister>().Dispatch(message,Color.red);
            
            }
            else
            {
                var request = new LoginWithEmailAddressRequest()
                {
                    Email = email,
                    Password =  pass ,
                };
                Player.email = request.Email;
                Player.passWord = request.Password;
                var message = "Login Success!";
                Signals.Get<SendMessageLoginRegister>().Dispatch(message,Color.green);
                PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
                SceneManager.LoadScene(ScreenIds.StartGameScene);
                 Debug.Log("LoginEnd");
            }
        }
        public void Register(string name, string email, string password)
        {
            var request = new RegisterPlayFabUserRequest()
            {
                DisplayName = name,
                Email = email,
                Password = password,
                RequireBothUsernameAndEmail = false,
            };
            Player.playerName = request.DisplayName;
            Player.email = request.Email;
            Player.passWord = request.Password;
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
        }

        private void OnRegisterFailure(PlayFabError obj)
        {
            Debug.Log("Dang ky fail " +obj.GenerateErrorReport());
            var message = "Register Fail!";
            Signals.Get<SendMessageLoginRegister>().Dispatch(message,Color.red);

        }

        private void OnRegisterSuccess(RegisterPlayFabUserResult obj)
        {
           var message = "Register Success!";
           Signals.Get<SendMessageLoginRegister>().Dispatch(message,Color.green);
            SaveDataPlayer();
        }

        private void OnLoginSuccess(LoginResult obj)
        {
            Debug.Log("Congratulations, you made your first successful API call!");
            GetDataPlayer();
        }

        private void OnLoginFailure(PlayFabError obj)
        {
            Debug.Log("Error logging in player with custom ID: " + obj.GenerateErrorReport());
        }
        #endregion
    }
}