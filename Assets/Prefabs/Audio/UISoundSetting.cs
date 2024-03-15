using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;

public class UISoundSetting : APanelController
{
    [SerializeField] private Button ButtonShowUISoundSetting;
    [SerializeField] private Button ButtonHideUISoundSetting;

    protected override void AddListeners()
    {
        base.AddListeners();
        ButtonHideUISoundSetting.onClick.AddListener(HideUISoundSetting);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ButtonHideUISoundSetting.onClick.RemoveListener(HideUISoundSetting);
    }
    private void HideUISoundSetting()
    {
        Signals.Get<CloseUISoundSetting>().Dispatch();
    }
}
