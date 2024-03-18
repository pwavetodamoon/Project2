using System;
using System.Collections;
using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Helper;
using  HHP.Ults.UIAnim;
using PlayFab_System;
using TMPro;

public class UILogin : APanelController
{
   [SerializeField] private Button _loginButton;
   [SerializeField] private Button _registerButton;
   [SerializeField] private Button _homeButton;
   [SerializeField] private Button _howToPlayButton;
   [SerializeField] private Button _aboutUsButton;
   [SerializeField] private TextMeshProUGUI _notificaltionText;
   [SerializeField] private TMP_InputField _emailInputfield;
   [SerializeField] private TMP_InputField _passWordlInputfield;

   private Transform[] _buttons ;


   private void Start()
   {
      _buttons = new Transform[] { _loginButton.transform, _homeButton.transform, _howToPlayButton.transform, _aboutUsButton.transform };
    UIAnim.ZoomInScale(_buttons);
   }

   protected override void AddListeners()
   {
      base.AddListeners();
      _loginButton.onClick.AddListener(() => OnLoginButtonClicked(_loginButton));
      _homeButton.onClick.AddListener(() => OnAllTestingButtonClicked(_homeButton));
      _howToPlayButton.onClick.AddListener(() => OnAllTestingButtonClicked(_howToPlayButton));
      _aboutUsButton.onClick.AddListener(() => OnAllTestingButtonClicked(_aboutUsButton));
      _registerButton.onClick.AddListener(() => OnRegisterButtonClicked(_registerButton));

   }
   protected override void RemoveListeners()
   {
      base.RemoveListeners();

      _loginButton.onClick.RemoveListener(() => OnLoginButtonClicked(_loginButton));
      _homeButton.onClick.RemoveListener(() => OnAllTestingButtonClicked(_homeButton));
      _howToPlayButton.onClick.RemoveListener(() => OnAllTestingButtonClicked(_howToPlayButton));
      _aboutUsButton.onClick.RemoveListener(() => OnAllTestingButtonClicked(_aboutUsButton));
      _registerButton.onClick.RemoveListener(() => OnRegisterButtonClicked(_registerButton));

   }
   private void OnAllTestingButtonClicked(Button button)
   {
      UIAnim.ZoomInOutScale(button.transform, Signals.Get<ShowUINotificaltion>().Dispatch);
   }
   private void OnLoginButtonClicked(Button button)
   {
      StartCoroutine(HandleLoginButtonClicked());

   }
   private void OnRegisterButtonClicked(Button button)
   {
      Debug.Log("test");
      PlayFabManager.Instance.Register(_emailInputfield.text, _passWordlInputfield.text);
      _notificaltionText.text = "Register Succesfull !";
      _notificaltionText.gameObject.SetActive(true);
  
   }
   private IEnumerator HandleLoginButtonClicked()
   {
      yield return new WaitForSeconds(0.2f);
     Signals.Get<OnLoginButtonClicked>().Dispatch(_emailInputfield.text, _passWordlInputfield.text);
   }
}
