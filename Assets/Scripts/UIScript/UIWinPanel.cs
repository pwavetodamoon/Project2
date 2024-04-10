using deVoid.UIFramework;
using deVoid.Utils;
using HHP.Ults.UIAnim;
using UnityEngine;
using UnityEngine.UI;
public class UIWinPanel : APanelController
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
        //Signals.Get<OpenSceneSelectStage>().Dispatch();
        Signals.Get<OpenStartGameScene>().Dispatch();


    }
    public void OnButtonRestart()
    {
        Time.timeScale = 1f;
        GameLevelControl.Instance.CheckOnWin();
        Signals.Get<CloseLosePanel>().Dispatch();
    }
}
