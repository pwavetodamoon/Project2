using deVoid.UIFramework;
using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameMainMenu;
    private void Awake()
    {
        _uIFrameMainMenu = _defaultUISetting.CreateUIInstance();
    }
    private void Start()
    {
        AddListener();
    }
    private void OnDestroy()
    {
        RemoveListener();
    }

    private void AddListener()
    {
        //_uIFrameMainMenu.ShowPanel(ScreenIds.UIMainMenu);
        Signals.Get<OpenUISoundSetting>().AddListener(ShowUISoundSetting);
        Signals.Get<CloseUISoundSetting>().AddListener(HideUISoundSetting);
    }
    private void RemoveListener()
    {
        Signals.Get<OpenUISoundSetting>().RemoveListener(ShowUISoundSetting);
        Signals.Get<CloseUISoundSetting>().RemoveListener(HideUISoundSetting);
    }

    private void ShowUISoundSetting()
    {
        _uIFrameMainMenu.ShowPanel(ScreenIds.UISoundSetting);
    }private void HideUISoundSetting()
    {
        _uIFrameMainMenu.HidePanel(ScreenIds.UISoundSetting);
    }
}
