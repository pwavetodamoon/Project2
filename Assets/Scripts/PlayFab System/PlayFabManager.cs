using System.Collections;
using System.Collections.Generic;
using System.Text;
using Currency;
using PlayFab;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;
using LoginResult = PlayFab.ClientModels.LoginResult;
using PlayFabError = PlayFab.PlayFabError;

namespace PlayFab_System
{
    public class PlayFabManager : MonoBehaviour
    {
        public PlayerData Player;
        public CurrencyManager currencyManager;

        public testfunc testfunc;

        private void Start()
        {
            StartCoroutine(Wait());
        }

        private void OnApplicationQuit()
        {
            SaveDataPlayer();
        }

        private IEnumerator Wait()
        {
            Login();
            yield return new WaitForSeconds(.2f);
            testfunc.Spawn();
        }


        [Button]
        private void GetDataPlayer()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnRevcievedData, OnDataSendError);
        }

        [Button]
        public void SaveRewardData()
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

        [Button]
        public void SaveDataPlayer()
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { "CusTomId", Player.customId },
                    { "Email", Player.email },
                    { "Password", Player.passWord },
                    { "Level", Player.levelPlayer.ToString() },
                    { "Gold", Player.gold.ToString() },
                    { "Hero Data", testfunc.ConvertToJson() }
                }
            };
            PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnDataSendError);
        }

        private void OnRevcievedData(GetUserDataResult result)
        {
            if (result.Data == null || !result.Data.ContainsKey("CusTomId"))
            {
                Debug.Log("No data found");
                return;
            }

            Player.customId = result.Data["CusTomId"].Value;
            Player.email = result.Data["Email"].Value;
            Player.passWord = result.Data["Password"].Value;
            Player.levelPlayer = int.Parse(result.Data["Level"].Value);
            Player.gold = int.Parse(result.Data["Gold"].Value);
            testfunc.ConvertJsonBack(result.Data["Hero Data"].Value);
            DebugResult(result);
        }

        private void DebugResult(GetUserDataResult result)
        {
            var sb = new StringBuilder();
            foreach (var item in result.Data) sb.Append(item.Key).Append(": ").Append(item.Value.Value).Append("\n");
            Debug.Log(sb.ToString());
            //Debug.Log(Player.customId + " " + Player.email + " " + Player.passWord + Player.levelPlayer + " " + Player.gold + " " + result.Data["Hero Data"].Value);
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

        private void Login()
        {
            Player = PlayerData.Instance;
            Player.customId = SystemInfo.deviceUniqueIdentifier;
            var request = new LoginWithCustomIDRequest
            {
                CustomId = Player.customId,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
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