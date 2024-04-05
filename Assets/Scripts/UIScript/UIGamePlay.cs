using System;
using deVoid.UIFramework;
using deVoid.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using HHP.Ults.UIAnim;
using TMPro;
using Core.Quest;

public class UIGamePlay : APanelController
{
    [SerializeField] private RectTransform _dFSArea;
    [SerializeField] private RectTransform _tourTimeLine;
    //t
    [SerializeField] private RectTransform _progessArea;

    [Header("UISlot Zone")]
    [SerializeField] private RectTransform _uISlot;
    [SerializeField] private Transform _currentTransform;
    [SerializeField] private Transform _targetTransform;
    [Header("Button")]
    [SerializeField] private Button ButtonShowMainMenu;
    [SerializeField] private Button _buttonUiSlot;
    [SerializeField] private Button _buttonPauseGame;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _cointText; 
    //
    [Header("Slider")]
    [SerializeField] private Slider _slider;
    
    [Header("Sprite")]
    [SerializeField] private Sprite _spriteOnButtonUiSlotl;
    [SerializeField] private Sprite _spriteOffButtonUiSlotl;
    [SerializeField] private Sprite _spriteOnUIPauseGame;
    [SerializeField] private Sprite _spriteOffUIPauseGame;

    private bool isShowUiSlots = true ;
    private bool isPaused = false ;


    private void Start()
    {
        //
        UIAnim.MoveDownUI(_progessArea);

        UIAnim.MoveDownUI(_dFSArea);
        UIAnim.MoveUIUp(_uISlot);
        UIAnim.MoveDownUI(_tourTimeLine);
    }
   

    protected override void AddListeners()
    {
        base.AddListeners();
        ButtonShowMainMenu.onClick.AddListener(ShowMainMenu);
        _buttonUiSlot.onClick.AddListener(ShowHideUiSlot);
        _buttonPauseGame.onClick.AddListener(PauseGame);
        Signals.Get<SendCurrency>().AddListener(SetCoinText);

        Signals.Get<SendProgessValue>().AddListener(SetSlider);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ButtonShowMainMenu.onClick.RemoveListener(ShowMainMenu);
        _buttonUiSlot.onClick.RemoveListener(ShowHideUiSlot);
        _buttonPauseGame.onClick.RemoveListener(PauseGame);
        Signals.Get<SendCurrency>().RemoveListener(SetCoinText);

        Signals.Get<SendProgessValue>().RemoveListener(SetSlider);
    }

    private void ShowDPS()
    {
        Signals.Get<ShowDPSMenu>().Dispatch();
    }
    private void ShowMainMenu()
    {
        Signals.Get<OpenUIMainMenu>().Dispatch();
    }
    private void PauseGame()
    {
        if (isPaused == false)
        {
            isPaused = true;
            _buttonPauseGame.image.sprite = _spriteOffUIPauseGame;
            Time.timeScale = 0;

        }
        
        else
        {
            isPaused = false;
            _buttonPauseGame.image.sprite = _spriteOnUIPauseGame;
            Time.timeScale = 1;
        }
    }
    private void ShowHideUiSlot()
    {
        if (isShowUiSlots == true)
        {
            isShowUiSlots = false;
            _buttonUiSlot.image.sprite = _spriteOnButtonUiSlotl;
            Debug.Log("isShowUiSlots1: " + isShowUiSlots);
            UIAnim.MoveUIToTarget(_uISlot.transform, _targetTransform);

        }

        else
        {
            isShowUiSlots = true;
            _buttonUiSlot.image.sprite = _spriteOffButtonUiSlotl;
            Debug.Log("isShowUiSlots2: " + isShowUiSlots);
            UIAnim.MoveUIToTarget(_uISlot.transform, _currentTransform);

        }
    }
    public void SetCoinText(int value)
    {

        _cointText.text = $"Total Reward: {value}";
        UIAnim.ZoomInOutScaleCusTom(_cointText.transform,1.1f);

    }
    public void SetSlider(float t)
    {
        //var value = QuestManager.Instance.stageInformation.pointCollected;
        //_slider.value = value;
        _slider.value = t;
        
    }
    // private void HideDPS()
    // {
    //     Signals.Get<HideDPSMenu>().Dispatch();
    // }
}
