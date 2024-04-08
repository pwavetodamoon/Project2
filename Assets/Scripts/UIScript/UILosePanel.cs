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
        restart.onClick.AddListener(OnbuttonBackToMenuClick);
        selectStage.onClick.AddListener(OnButtonRestart);

    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        restart.onClick.RemoveListener(OnbuttonBackToMenuClick);
        selectStage.onClick.RemoveListener(OnButtonRestart);
    }

    public void OnbuttonBackToMenuClick()
    {
        UIAnim.ZoomOutScale(this.transform, Signals.Get<OpenSceneSelectStage>().Dispatch) ;
    }
    public void OnButtonRestart() 
    {
        UIAnim.ZoomOutScale(this.transform, Signals.Get<CloseLosePanel>().Dispatch);
    } 
   

}
