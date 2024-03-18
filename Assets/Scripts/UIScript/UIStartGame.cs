using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using deVoid.Utils;
using PlayFab_System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStartGame : APanelController
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _levelPlayer;



    private void Start()
    {
        // _playerName.text = $"PLAYER NAME :{PlayFabManager.Instance.Player.playerName} ";
        // _levelPlayer.text = $"PLAYER NAME :{PlayFabManager.Instance.Player.levelPlayer} ";
        // _playerName.text = "PLAYER NAME : GameDev" ;
        // _levelPlayer.text = "Level : 1 ";
        StartCoroutine(GetInfoPlayer());


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
        Debug.Log("OnPlayButtonOnClicked");
        Signals.Get<OpenSceneGamePlay>().Dispatch();
    }

    private void OnShopButtonOnClicked()
    {
        Signals.Get<OpenShop>().Dispatch();
    }

    private IEnumerator GetInfoPlayer()
    {
        yield return new WaitForSeconds(0.25f);
        _playerName.text = $"NAME :{PlayFabManager.Instance.Player.playerName} ";
        _levelPlayer.text = $"Level :{PlayFabManager.Instance.Player.levelPlayer} ";
    }
}
