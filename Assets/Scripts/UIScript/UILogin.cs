using System;
using System.Collections;
using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
      InitializeTween();
   }

   protected override void AddListeners()
   {
      base.AddListeners();
      _loginButton.onClick.AddListener(() =>
      {
         Sequence sequence = DOTween.Sequence();

         sequence.Append(_loginButton.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).SetEase(Ease.Linear));

         sequence.Append(_loginButton.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.Linear)).OnComplete(() =>
         {
            OnLoginButtonClicked();
         });
      });

   }
   protected override void RemoveListeners()
   {
      base.RemoveListeners();
      _loginButton.onClick.RemoveListener(() =>
      {
         OnLoginButtonClicked();
      });

   }
   void OnDestroy()
   {
      DOTween.KillAll();
   }

   private void OnLoginButtonClicked()
   {
      StartCoroutine(HandleLoginButtonClicked());

   }
   private IEnumerator HandleLoginButtonClicked()
   {
      yield return new WaitForSeconds(0.2f);
     Signals.Get<OnLoginButtonClicked>().Dispatch();
   }
   void InitializeTween()
   {
      foreach (Transform buttonTransform in _buttons)
      {
         buttonTransform.localScale = Vector3.zero;

         buttonTransform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBack);
      }
   }
   
}
