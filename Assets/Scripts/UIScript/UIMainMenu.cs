using deVoid.Utils;
using deVoid.UIFramework;
using UnityEngine.UI;
using UnityEngine;
using HHP.Ults.UIAnim;

public class UIMainMenu : APanelController
{
    [SerializeField] private Button ButtonHideUIMainMenu;
    [SerializeField] private Button ButtonShowUISoundSetting;
    [SerializeField] private Button ButtonActiveNewGame;
    [SerializeField] private Button ButtonActiveQuitAndSave;

    private void Start()
    {
        UIAnim.ZoomInOutScale(this.transform);
    }
    private void OnEnable()
    {
        UIAnim.ZoomInOutScale(this.transform);

    }
    protected override void AddListeners()
    {
        base.AddListeners();
        ButtonHideUIMainMenu.onClick.AddListener(HideUIMainMenu);

        ButtonShowUISoundSetting.onClick.AddListener(ShowUISoundSetting);
        ButtonActiveQuitAndSave.onClick.AddListener(ShowStartGameScene);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ButtonHideUIMainMenu.onClick.RemoveListener(HideUIMainMenu);

        ButtonShowUISoundSetting.onClick.RemoveListener(ShowUISoundSetting);
        ButtonActiveQuitAndSave.onClick.RemoveListener(ShowStartGameScene);

    }
    private void HideUIMainMenu()
    {
        UIAnim.ZoomOutScale(this.transform, Signals.Get<HideUIMainMenu>().Dispatch);
    } 
    private void ShowUISoundSetting()
    {
        Signals.Get<OpenUISoundSetting>().Dispatch();
    }
    private void ShowStartGameScene() 
    { 
        Signals.Get<OpenTest>().Dispatch();
    }
    
   


}
