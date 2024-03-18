using deVoid.UIFramework;
using deVoid.Utils;
using PlayFab_System;
using UnityEngine;

public class UIGamePlayController : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameGamePlay;
    private void Awake()
    {
        _uIFrameGamePlay = _defaultUISetting.CreateUIInstance();
    }
    private void Start()
    {
        PlayFabManager.Instance.InitResource();
        AddListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }
    private void AddListener()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UIGamePlay);
        Signals.Get<ShowDPSMenu>().AddListener(ShowUIDPS);
        Signals.Get<HideDPSMenu>().AddListener(HideUIDPS);
        //-----
        Signals.Get<OpenUIMainMenu>().AddListener(ShowUIMainMenu);
        Signals.Get<HideUIMainMenu>().AddListener(HideUIMainMenu);
        //
        Signals.Get<OpenUISoundSetting>().AddListener(ShowUISoundSetting);
        Signals.Get<CloseUISoundSetting>().AddListener(HideUISoundSetting);
    }

    private void RemoveListener()
    {
        Signals.Get<HideDPSMenu>().RemoveListener(HideUIDPS);
        Signals.Get<ShowDPSMenu>().RemoveListener(ShowUIDPS);

        //------
        Signals.Get<OpenUIMainMenu>().RemoveListener(ShowUIMainMenu);
        Signals.Get<HideUIMainMenu>().RemoveListener(HideUIMainMenu);
        
        //
        Signals.Get<OpenUISoundSetting>().AddListener(ShowUISoundSetting);
        Signals.Get<CloseUISoundSetting>().AddListener(HideUISoundSetting);
        
        
        //
        Signals.Get<OpenUISoundSetting>().RemoveListener(ShowUISoundSetting);
        Signals.Get<CloseUISoundSetting>().RemoveListener(HideUISoundSetting);
    }

    private void ShowUIDPS()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UIDPS);
    }
    private void HideUIDPS()
    {
        _uIFrameGamePlay.HidePanel(ScreenIds.UIDPS);
    }
    //-------
    private void ShowUIMainMenu()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UIMainMenu);
    }
    private void HideUIMainMenu()
    {
        _uIFrameGamePlay.HidePanel(ScreenIds.UIMainMenu);
    }
    
    private void ShowUISoundSetting()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UISoundSetting);
    }private void HideUISoundSetting()
    {
        _uIFrameGamePlay.HidePanel(ScreenIds.UISoundSetting);
    }
}
