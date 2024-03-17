using System;
using deVoid.UIFramework;
using deVoid.Utils;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using  HHP.Ults.UIAnim;

public class NotificationUI : APanelController
{
   [SerializeField] private TextMeshProUGUI _textNotification;
   [SerializeField] private GameObject _backGround;
   [SerializeField] private Button _buttonOkay;
   private Transform[] _transformsGameObjArrays;

   private void Start()
   {
      _textNotification.text = "Coming Soon...";
      _transformsGameObjArrays = new [] { _textNotification.transform, _backGround.transform, _buttonOkay.transform};
      UIAnim.ZoomInScale(_transformsGameObjArrays);

   }
   protected override void AddListeners()
   {
      base.AddListeners();
      _buttonOkay.onClick.AddListener( OnButtonOkayClicked);

   }
   protected override void RemoveListeners()
   {
      base.RemoveListeners();
      _buttonOkay.onClick.RemoveListener( OnButtonOkayClicked);
   }

   private void OnEnable()
   {
      Debug.Log("OnEnable");
      this.transform.localScale = Vector3.one;
      if (gameObject.activeSelf)
      {
         Debug.Log("true");
         UIAnim.ZoomInScale(_transformsGameObjArrays);
      }
   }
   private void OnButtonOkayClicked( )
   {
      UIAnim.ZoomOutScale(this.transform, Signals.Get<HideUINotificaltion>().Dispatch);
   }
}
