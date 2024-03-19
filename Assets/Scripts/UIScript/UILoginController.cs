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
        Signals.Get<OnLoginButtonClicked>().AddListener(OpenSceneStartGame);
        Signals.Get<ShowUINotificaltion>().AddListener(OpenUINotificaltion);
        Signals.Get<HideUINotificaltion>().AddListener(HideUINotification);

    }
    private void RemoveListener()
    {
        Signals.Get<OnLoginButtonClicked>().RemoveListener(OpenSceneStartGame);
        Signals.Get<ShowUINotificaltion>().RemoveListener(OpenUINotificaltion);
        Signals.Get<HideUINotificaltion>().RemoveListener(HideUINotification);

    }
    
    private void OpenSceneStartGame(string email , string pass)
    {
        PlayFabManager.Instance.StartCoroutine(email,pass);
        SceneManager.LoadScene(ScreenIds.StartGameScene);
    }
    private void OpenUINotificaltion()
    {
        _uIFrameLogin.ShowPanel(ScreenIds.NotificationUI);
    }

    private void HideUINotification()
    {
        _uIFrameLogin.HidePanel(ScreenIds.NotificationUI);

    }

   
}
