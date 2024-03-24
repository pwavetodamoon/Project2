using System;
using deVoid.UIFramework;
using deVoid.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using HHP.Ults.UIAnim;
using TMPro;

public class UIGamePlay : APanelController
{
    public Button ButtonShowDPS;
    public Button ButtonHidesDPS;
    [SerializeField] private RectTransform _dFSArea;
    [SerializeField] private RectTransform _tourTimeLine;

    [Header("UISlot Zone")]
    [SerializeField] private RectTransform _uISlot;
    [SerializeField] private Transform _currentTransform;
    [SerializeField] private Transform _targetTransform;
    [Header("Button")]
    [SerializeField] private Button ButtonShowMainMenu;
    [SerializeField] private Button _buttonUiSlot;
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _cointText;
    [Header("Sprite")]
    [SerializeField] private Sprite _spriteOnButtonUiSlotl;
    [SerializeField] private Sprite _spriteOffButtonUiSlotl;

    private bool isShowUiSlots = true ;

    private void Start()
    {
       UIAnim.MoveDownUI(_dFSArea);
        UIAnim.MoveUIUp(_uISlot);
      UIAnim.MoveDownUI(_tourTimeLine);
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        ButtonShowDPS.onClick.AddListener(ShowDPS);
        ButtonShowMainMenu.onClick.AddListener(ShowMainMenu);
        _buttonUiSlot.onClick.AddListener(ShowHideUiSlot);
        Signals.Get<SendCurrency>().AddListener(SetCoinText);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ButtonShowDPS.onClick.RemoveListener(ShowDPS);
        ButtonShowMainMenu.onClick.RemoveListener(ShowMainMenu);
        _buttonUiSlot.onClick.RemoveListener(ShowHideUiSlot);
        Signals.Get<SendCurrency>().RemoveListener(SetCoinText);
    }

    private void ShowDPS()
    {
        Signals.Get<ShowDPSMenu>().Dispatch();
    }
    private void ShowMainMenu()
    {
        Signals.Get<OpenUIMainMenu>().Dispatch();
    }

    private void ShowHideUiSlot()
    {
        if (isShowUiSlots == true)
        {
            isShowUiSlots = false;
            _buttonUiSlot.image.sprite = _spriteOnButtonUiSlotl;
            Debug.Log("isShowUiSlots1: " +isShowUiSlots);
            UIAnim.MoveUIDownCustom(_uISlot.transform, _targetTransform);

        }
        
        else
        {
            isShowUiSlots = true;
          _buttonUiSlot.image.sprite = _spriteOffButtonUiSlotl;
          Debug.Log("isShowUiSlots2: " +isShowUiSlots);
          UIAnim.MoveUIDownCustom(_uISlot.transform, _currentTransform);
        
        }
     //   Debug.Log("value : " + -300f );
    }
    public void SetCoinText(int value)
    {
        Debug.Log(("SetCoinText"));
        _cointText.text = value.ToString();
    }
    // private void HideDPS()
    // {
    //     Signals.Get<HideDPSMenu>().Dispatch();
    // }
}
