using CombatSystem.HeroDataManager;
using deVoid.UIFramework;
using deVoid.Utils;
using PlayFab_System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGamePlayController : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameGamePlay;
    [SerializeField] StageInformation _stageInformation; 
    [SerializeField] private HeroManager heroManager;
    private void OnValidate()
    {

        if (_stageInformation == null)
        {
            _stageInformation = GetDataSupport.Get().StageInformation;
        }
        if (heroManager == null)
        {
            heroManager = GetDataSupport.Get().HeroManager;
        }
        _uIFrameGamePlay = _defaultUISetting.CreateUIInstance();
    }
   
    private void Start()
    {
        AddListener();
        Invoke("Test", 1f);
    }
    private void Test()
    {
        PlayFabManager.Instance.InitResource();

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
        //
        Signals.Get<OpenTest>().AddListener(ShowStartGameScene);
        Signals.Get<OpenLosePanel>().AddListener(OpenLosePanels);
        Signals.Get<ClosePanel>().AddListener(RestartGame);
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
        //
        Signals.Get<OpenTest>().RemoveListener(ShowStartGameScene);
        Signals.Get<OpenLosePanel>().RemoveListener(OpenLosePanels);
        Signals.Get<ClosePanel>().RemoveListener(RestartGame);
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
    }
    private void HideUISoundSetting()
    {
        _uIFrameGamePlay.HidePanel(ScreenIds.UISoundSetting);
    }
    private void ShowStartGameScene()
    {
        SceneManager.LoadScene(ScreenIds.StartGameScene);
    }
    private void RestartGame()
    {
        //_uIFrameGamePlay.HidePanel(ScreenIds.UILosePanel);
        SceneManager.LoadScene(ScreenIds.TestScene);
        _stageInformation.ResetStage();
        
    }
    private void OpenLosePanels()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UILosePanel);
    }
    private void OpenWinPanels()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UILosePanel);
    }
}
