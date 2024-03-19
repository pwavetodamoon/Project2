using System;
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
        Signals.Get<OpenSceneSelectStage>().Dispatch();
        Debug.Log("OnPlayButtonOnClicked");
        Signals.Get<OpenSceneGamePlay>().Dispatch();
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
        _playerName.text = name;
        _levelPlayer.text = level.ToString();
    }
    
}
