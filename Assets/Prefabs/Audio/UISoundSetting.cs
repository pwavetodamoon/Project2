using System;
using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;
using HHP.Ults.UIAnim;

public class UISoundSetting : APanelController
{
    [SerializeField] private Button ButtonShowUISoundSetting;
    [SerializeField] private Button ButtonHideUISoundSetting;
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
        ButtonHideUISoundSetting.onClick.AddListener(HideUISoundSetting);
    }
    
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ButtonHideUISoundSetting.onClick.RemoveListener(HideUISoundSetting);
    }
    private void HideUISoundSetting()
    {
        UIAnim.ZoomOutScale(this.transform,Signals.Get<CloseUISoundSetting>().Dispatch);
    }
}
