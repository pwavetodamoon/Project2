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
        Time.timeScale = 1.0f;
        Signals.Get<OpenSceneSelectStage>().Dispatch() ;
    }
    public void OnButtonRestart() 
    {
        GameLevelControl.Instance.CheckOnLoose();
        Time.timeScale = 1f ;
        Signals.Get<CloseLosePanel>().Dispatch();
    } 
   

}
