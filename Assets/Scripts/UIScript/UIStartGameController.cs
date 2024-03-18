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
    }
    private void RemoveListener()
    {
        Signals.Get<OpenSceneGamePlay>().RemoveListener(OpenSceneGamePlay);
   
    }
    private void OpenSceneGamePlay()
    {
        SceneManager.LoadScene(ScreenIds.TestScene);
    }
}
