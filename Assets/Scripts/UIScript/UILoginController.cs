using deVoid.UIFramework;
using deVoid.Utils;
using PlayFab_System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoginController : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameLogin;
    
    private void Awake()
    {
        _uIFrameLogin = _defaultUISetting.CreateUIInstance();
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
        _uIFrameLogin.ShowPanel(ScreenIds.UILogin);
        Signals.Get<OnLoginButtonClicked>().AddListener(OpenSceneGamePlay);
    }
    private void RemoveListener()
    {
        Signals.Get<OnLoginButtonClicked>().RemoveListener(OpenSceneGamePlay);
    }

    private void OpenSceneGamePlay()
    {
        PlayFabManager.Instance.StartCoroutine();
        SceneManager.LoadScene(ScreenIds.TestScene);
    }

   
}
