using System;
using System.Collections;
using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Helper;
using Ults;

public class UILogin : APanelController
{
   [SerializeField] private Button _loginButton;
   [SerializeField] private Button _homeButton;
   [SerializeField] private Button _howToPlayButton;
   [SerializeField] private Button _aboutUsButton;
   private Transform[] _buttons ;


   private void Start()
   {
      _buttons = new Transform[] { _loginButton.transform, _homeButton.transform, _howToPlayButton.transform, _aboutUsButton.transform };
      Ults.UIAnim.InScale(_buttons);
   }

   protected override void AddListeners()
   {
      base.AddListeners();
      _loginButton.onClick.AddListener(() => OnLoginButtonClicked(_loginButton));
      _homeButton.onClick.AddListener(() => OnAllTestingButtonClicked(_homeButton));
      _howToPlayButton.onClick.AddListener(() => OnAllTestingButtonClicked(_howToPlayButton));
      _aboutUsButton.onClick.AddListener(() => OnAllTestingButtonClicked(_aboutUsButton));

   }
   protected override void RemoveListeners()
   {
      base.RemoveListeners();

      _loginButton.onClick.RemoveListener(() => OnLoginButtonClicked(_loginButton));
      _homeButton.onClick.RemoveListener(() => OnAllTestingButtonClicked(_homeButton));
      _howToPlayButton.onClick.RemoveListener(() => OnAllTestingButtonClicked(_howToPlayButton));
      _aboutUsButton.onClick.RemoveListener(() => OnAllTestingButtonClicked(_aboutUsButton));

   }
   private void OnAllTestingButtonClicked(Button button)
   {
      UIAnim.ScaleInOut(button, Signals.Get<ShowUINotificaltion>().Dispatch);
   }
   private void OnLoginButtonClicked(Button button)
   {
      StartCoroutine(HandleLoginButtonClicked());

   }
   private IEnumerator HandleLoginButtonClicked()
   {
      yield return new WaitForSeconds(0.2f);
     Signals.Get<OnLoginButtonClicked>().Dispatch();
   }
}
