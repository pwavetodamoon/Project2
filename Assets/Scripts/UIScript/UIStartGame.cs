using System.Collections;
using deVoid.UIFramework;
using deVoid.Utils;
using PlayFab_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class UIStartGame : APanelController
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _levelPlayer;



    private void Start()
    {
        Test2();
        //StartCoroutine(GetInfoPlayer());
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
        yield return new WaitForSeconds(1f);
        _playerName.text = $"NAME :{PlayFabManager.Instance.Player.playerName} ";
        _levelPlayer.text = $"Level :{PlayFabManager.Instance.Player.levelPlayer} ";
    }
    [Button]
    private void Test2()
    {
        _playerName.text = $"NAME :{PlayFabManager.Instance.Player.playerName} ";
        _levelPlayer.text = $"Level :{PlayFabManager.Instance.Player.levelPlayer} ";
    }
}
