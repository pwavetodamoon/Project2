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
using UnityEngine.Serialization;

public class UILogin : APanelController
{

   [SerializeField] private GameObject _uIRegister;
   [SerializeField] private Button _loginButton;
   [SerializeField] private Button _registerButton;
   [SerializeField] private Button _okayRegisterButton;
   [SerializeField] private Button _homeButton;
   [SerializeField] private Button _howToPlayButton;
   [SerializeField] private Button _aboutUsButton;
   [SerializeField] private TextMeshProUGUI _notificaltionText;
   //Text Login
   [SerializeField] private TMP_InputField _emailInputfieldLogin;
   [SerializeField] private TMP_InputField _passWordlInputfieldLogin;
   //Text Register
   [SerializeField] private TMP_InputField _nameInputfieldRegister;
   [SerializeField] private TMP_InputField _emailInputfieldRegister;
   [SerializeField] private TMP_InputField _passWordlInputfieldRegister;

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
      _okayRegisterButton.onClick.AddListener(() => OnOkayRegisterButtonClicked(_okayRegisterButton));

   }
   protected override void RemoveListeners()
   {
      base.RemoveListeners();

      _loginButton.onClick.RemoveListener(() => OnLoginButtonClicked(_loginButton));
      _homeButton.onClick.RemoveListener(() => OnAllTestingButtonClicked(_homeButton));
      _howToPlayButton.onClick.RemoveListener(() => OnAllTestingButtonClicked(_howToPlayButton));
      _aboutUsButton.onClick.RemoveListener(() => OnAllTestingButtonClicked(_aboutUsButton));
      _registerButton.onClick.RemoveListener(() => OnRegisterButtonClicked(_registerButton));
      _okayRegisterButton.onClick.RemoveListener(() => OnOkayRegisterButtonClicked(_okayRegisterButton));

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
      UIAnim.ZoomInOutScale(_uIRegister.transform);
      _uIRegister.SetActive(true);
   }

   private void OnOkayRegisterButtonClicked(Button button)
   {
      PlayFabManager.Instance.Register(_nameInputfieldRegister.text,_emailInputfieldRegister.text, _passWordlInputfieldRegister.text);
      _uIRegister.SetActive(false);
   }
   private IEnumerator HandleLoginButtonClicked()
   {
      yield return new WaitForSeconds(0.2f);
      Debug.Log("HandleLoginButtonClicked");
     Signals.Get<OnLoginButtonClicked>().Dispatch(_emailInputfieldLogin.text, _passWordlInputfieldLogin.text);
   }
   public IEnumerator HandleTextNotificaltion(string notificaltion , Color color)
   {
      Debug.Log("HandleTextNotificaltion");

      _notificaltionText.text = notificaltion;
      _notificaltionText.color =color;
      _notificaltionText.gameObject.SetActive(true);
      yield return  new WaitForSeconds(2f);
      _notificaltionText.gameObject.SetActive(false);

   }
}
