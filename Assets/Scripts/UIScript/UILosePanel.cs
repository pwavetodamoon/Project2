using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.UIFramework;
using UnityEngine.UI;
using HHP.Ults.UIAnim;
using deVoid.Utils;

public class UILosePanel : APanelController
{
    [SerializeField] private Button selectStage;
    [SerializeField] private Button restart;
    private void Start()
    {
        Time.timeScale = 0;
    }
    protected override void AddListeners()
    {
        base.AddListeners();
        restart.onClick.AddListener(OnButtonRestart);
        selectStage.onClick.AddListener(OnbuttonBackToMenuClick);

    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        restart.onClick.RemoveListener(OnButtonRestart);
        selectStage.onClick.RemoveListener(OnbuttonBackToMenuClick);
    }

    public void OnbuttonBackToMenuClick()
    {
        Time.timeScale = 1;
        UIAnim.ZoomOutScale(this.transform, Signals.Get<OpenSceneSelectStage>().Dispatch);
    }
    public void OnButtonRestart()
    {
        Time.timeScale = 1;
        UIAnim.ZoomOutScale(this.transform, Signals.Get<ClosePanel>().Dispatch);
    }


}
