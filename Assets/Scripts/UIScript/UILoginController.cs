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
        Signals.Get<ShowUINotificaltion>().AddListener(OpenUINotificaltion);
        Signals.Get<HideUINotificaltion>().AddListener(HideUINotification);

    }
    private void RemoveListener()
    {
        Signals.Get<OnLoginButtonClicked>().RemoveListener(OpenSceneGamePlay);
        Signals.Get<ShowUINotificaltion>().RemoveListener(OpenUINotificaltion);
        Signals.Get<HideUINotificaltion>().RemoveListener(HideUINotification);

    }

    private void OpenSceneGamePlay()
    {
        PlayFabManager.Instance.StartCoroutine();
        SceneManager.LoadScene(ScreenIds.TestScene);
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
