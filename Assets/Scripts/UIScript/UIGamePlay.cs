using System;
using deVoid.UIFramework;
using deVoid.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using HHP.Ults.UIAnim;

public class UIGamePlay : APanelController
{
    public Button ButtonShowDPS;
    public Button ButtonHidesDPS;
    [SerializeField] private RectTransform _dFSArea;
    [SerializeField] private RectTransform _uISlot;
    [SerializeField] private RectTransform _tourTimeLine;


    [SerializeField] private Button ButtonShowMainMenu;

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
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ButtonShowDPS.onClick.RemoveListener(ShowDPS);

        ButtonShowMainMenu.onClick.RemoveListener(ShowMainMenu);
    }

    private void ShowDPS()
    {
        Signals.Get<ShowDPSMenu>().Dispatch();
    }
    private void ShowMainMenu()
    {
        Signals.Get<OpenUIMainMenu>().Dispatch();
    }

    // private void HideDPS()
    // {
    //     Signals.Get<HideDPSMenu>().Dispatch();
    // }
}
