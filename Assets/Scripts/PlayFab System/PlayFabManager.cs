using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace PlayFab_System
{
    public class PlayFabManager : MonoBehaviour
    {
        public  PlayerData Player;
        private void Start()
        {
            Player = PlayerData.Instance;
            Login();
        }

        #region Login

        private void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        }

        private void OnLoginSuccess(LoginResult obj)
        {
            Debug.Log("Congratulations, you made your first successful API call!");
        }

        private void OnLoginFailure(PlayFabError obj)
        {
            Debug.Log("Error logging in player with custom ID: " + obj.GenerateErrorReport());
        }

        #endregion
        
        [Button]
        private void GetDataPlayer()
        {
            PlayFabClientAPI.GetUserData( new GetUserDataRequest() , OnRevcievedData, OnDataSendError);
        }

        // Save And GEt Player Data 
        [Button]
        public void SaveDataPlayer()
        {
            var request = new UpdateUserDataRequest
            {        
                Data = new Dictionary<string, string>
                {
                    { "CusTomId" , Player.customId },
                    { "Email" ,Player.email },
                    { "Password" , Player.passWord }
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
            else
            {
                Debug.Log("Data found");
                Player.customId = result.Data["CusTomId"].Value;
                Player.email = result.Data["Email"].Value;
                Player.passWord = result.Data["Password"].Value;
                Debug.Log(  Player.customId + " " + Player.email + " " + Player.passWord);

            }
        }
        private void OnDataSend(UpdateUserDataResult result)
        {
            Debug.Log("Data sent to PlayFab");
        }
        private void OnDataSendError (PlayFabError error)
        {
            Debug.Log("Error sending data to PlayFab: " + error.GenerateErrorReport());
        }
    }
}