using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using deVoid.Utils;
using PlayFab_System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartGameController : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameStartGame;

    private void Awake()
    {
        _uIFrameStartGame = _defaultUISetting.CreateUIInstance();
    }
    private void Start()
    {
        AddListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }
    private void AddListener()
    {
        _uIFrameStartGame.ShowPanel(ScreenIds.UIStartGame);
        Signals.Get<OpenSceneGamePlay>().AddListener(OpenSceneGamePlay);
        Signals.Get<OpenShop>().AddListener(OpenShop);
        Signals.Get<HideShop>().AddListener(HideShop);
        Signals.Get<OpenSceneSelectStage>().AddListener(OpenSceneSelectStage);
        Signals.Get<HideStageSelectionUI>().AddListener(HideStageSelectionUI);
    }
    private void RemoveListener()
    {
        Signals.Get<OpenSceneGamePlay>().RemoveListener(OpenSceneGamePlay);
        Signals.Get<OpenShop>().RemoveListener(OpenShop);
        Signals.Get<HideShop>().RemoveListener(HideShop);
        Signals.Get<OpenSceneSelectStage>().RemoveListener(OpenSceneSelectStage);
        Signals.Get<HideStageSelectionUI>().RemoveListener(HideStageSelectionUI);
    }
    private void HideStageSelectionUI()
    {
        _uIFrameStartGame.HidePanel(ScreenIds.StageSelectionUI);
    }
    private void OpenSceneGamePlay()
    {
       //PlayFabManager.Instance.StartCoroutine(,);
       SceneManager.LoadScene(ScreenIds.TestScene);
    }
    private void OpenSceneSelectStage()
    {
        _uIFrameStartGame.ShowPanel(ScreenIds.StageSelectionUI);
    }
    private void OpenShop()
    {
        _uIFrameStartGame.ShowPanel(ScreenIds.UIShop);
    }
    private void HideShop()
    {
        _uIFrameStartGame.HidePanel(ScreenIds.UIShop);
    }

}
    
