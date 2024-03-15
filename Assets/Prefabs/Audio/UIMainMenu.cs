using deVoid.Utils;
using deVoid.UIFramework;
using UnityEngine.UI;
using UnityEngine;

public class UIMainMenu : APanelController
{
    [SerializeField] private Button ButtonShowUISoundSetting;
    [SerializeField] private Button ButtonActiveNewGame;
    [SerializeField] private Button ButtonActiveQuitAndSave;

    [SerializeField] private Button ButtonHideUIMainMenu;

    protected override void AddListeners()
    {
        base.AddListeners();
        ButtonHideUIMainMenu.onClick.AddListener(HideUIMainMenu);

        ButtonShowUISoundSetting.onClick.AddListener(ShowUISoundSetting);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ButtonHideUIMainMenu.onClick.RemoveListener(HideUIMainMenu);

        ButtonShowUISoundSetting.onClick.RemoveListener(ShowUISoundSetting);
    }
    private void HideUIMainMenu()
    {
        Signals.Get<HideUIMainMenu>().Dispatch();
    } 
    private void ShowUISoundSetting()
    {
        Signals.Get<OpenUISoundSetting>().Dispatch();
    }
   


}
