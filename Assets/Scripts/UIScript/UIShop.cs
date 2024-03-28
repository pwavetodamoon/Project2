using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using deVoid.Utils;
using PlayFab_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : APanelController
{
    [SerializeField] private Button _buttonClose;
    [SerializeField] private TextMeshProUGUI _goldText;

    protected override void Awake()
    {
        base.Awake();
        InitResource();
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        _buttonClose.onClick.AddListener(CloseShop);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        _buttonClose.onClick.RemoveListener(CloseShop);
        
    }
    private void CloseShop()
    {
        Signals.Get<HideShop>().Dispatch();
    }

    private void InitResource()
    {
        _goldText.text = PlayFabManager.Instance.Player.gold.ToString();
    }
}
