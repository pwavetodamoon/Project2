using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.UIFramework;
using UnityEngine.UI;
using deVoid.Utils;
using HHP.Ults.UIAnim;


public class UIStage : APanelController
{
    [SerializeField] private Button ButtonBackToStartGameScene;
    [SerializeField] private Button ButtonNextPage;
    [SerializeField] private Button ButtonPreviousPage;
    [SerializeField] private Button Stage_1;
    [SerializeField] private Button Stage_2;
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
        Stage_1.onClick.AddListener(Stage1);
        ButtonBackToStartGameScene.onClick.AddListener(HideStageSelectionUI);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        Stage_1.onClick.RemoveListener(Stage1);
        ButtonBackToStartGameScene.onClick.RemoveListener(HideStageSelectionUI);
    }
    private void Stage1()
    {
      Signals.Get<OpenSceneGamePlay>().Dispatch();
    }
    private void HideStageSelectionUI()
    {
        UIAnim.ZoomOutScale(this.transform, Signals.Get<HideStageSelectionUI>().Dispatch);
    }
}
