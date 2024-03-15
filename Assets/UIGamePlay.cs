using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : APanelController
{
    public Button ButtonShowDPS;
    public Button ButtonHidesDPS;

    [SerializeField] private Button ButtonShowMainMenu;

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
