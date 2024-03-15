using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : APanelController
{
   [SerializeField] private Button _loginButton;

   protected override void AddListeners()
   {
      base.AddListeners();
      _loginButton.onClick.AddListener(OnLoginButtonClicked);
   }
   protected override void RemoveListeners()
   {
      base.RemoveListeners();
      _loginButton.onClick.RemoveListener(OnLoginButtonClicked);
   }
   private void OnLoginButtonClicked()
   {
      Signals.Get<OnLoginButtonClicked>().Dispatch();
   }

}
