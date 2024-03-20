using deVoid.UIFramework;
using deVoid.Utils;
using Helper;
using PlayFab_System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoginController : Singleton<UILoginController>
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameLogin;
    [SerializeField] private UILogin _uiLogin;
    
    private void Awake()
    {
        _uIFrameLogin = _defaultUISetting.CreateUIInstance();
    }
    private void Start()
    {
        AddListener();
        _uiLogin = FindObjectOfType<UILogin>();
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
        PlayFabManager.Instance.WaitLogin(email,pass);
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

    public void RegisterNotification(string notification, Color color)
    {
        StartCoroutine(_uiLogin.HandleTextNotificaltion(notification,color));
    }
    public void LoginNotification(string notification, Color color)
    {
        StartCoroutine(_uiLogin.HandleTextNotificaltion(notification,color));
    }

   
}
