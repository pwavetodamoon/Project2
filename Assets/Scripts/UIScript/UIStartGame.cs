using System.Collections;
using System.Threading.Tasks;
using deVoid.UIFramework;
using deVoid.Utils;
using PlayFab_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using HHP.Ults.UIAnim;

public class UIStartGame : APanelController
{
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _levelPlayer;
    [SerializeField] private TextMeshProUGUI _goldtxt;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Transform _uiDailyMission;
    [SerializeField] private Transform _uiHandelCurrency;
    [SerializeField] private Transform _uiHandlePlayerinfo;
    
    private Transform[] _uiArrays;
    private void Start()
    {
        LoadUIsStartGame();
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        _playButton.onClick.AddListener(OnPlayButtonOnClicked);
        _shopButton.onClick.AddListener(OnShopButtonOnClicked);

    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        _playButton.onClick.RemoveListener(OnPlayButtonOnClicked);
        _shopButton.onClick.RemoveListener(OnShopButtonOnClicked);
    }

    private void OnPlayButtonOnClicked()
    {
        //Signals.Get<OpenSceneGamePlay>().Dispatch();
        Signals.Get<OpenSceneSelectStage>().Dispatch();
        Debug.Log("OnPlayButtonOnClicked");
    }

    private void OnShopButtonOnClicked()
    {
        Signals.Get<OpenShop>().Dispatch();
    }

    private IEnumerator GetInfoPlayer()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("GetInfoPlayer coroutine");
        var name = PlayFabManager.Instance.Player.playerName;
        var level = PlayFabManager.Instance.Player.levelPlayer;
        var gold = PlayFabManager.Instance.Player.gold;

        _playerName.text = name;
        _levelPlayer.text = level.ToString();
        _goldtxt.text = $":{gold.ToString()}";
        Debug.Log($" _playerName.text: { name} ....  _playerName.level{level} ... _gold.text: { gold}");
    }
    private async Task GetInfoPlayerAsync()
    {
        Debug.Log("GetInfoPlayer coroutine");
        var name = PlayFabManager.Instance.Player.playerName;
        var level = PlayFabManager.Instance.Player.levelPlayer;
        var gold = PlayFabManager.Instance.Player.gold;

        _playerName.text = name;
        _levelPlayer.text = level.ToString();
        _goldtxt.text = $":{gold.ToString()}";
        await Task.Yield();
    }
    private async void LoadUIsStartGame()
    {
        await GetInfoPlayerAsync();
        _uiArrays = new []{_playButton.transform,_shopButton.transform, _uiDailyMission,_uiHandelCurrency,_uiHandlePlayerinfo};
        UIAnim.ZoomInScaleUIArray(_uiArrays);
    }
    
}
