using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;    

public class UIGamePlay : APanelController
{
    public Button ButtonShowDPS;
    public Button ButtonHidesDPS;

    protected override void AddListeners()
    {
        base.AddListeners();
        ButtonShowDPS.onClick.AddListener(ShowDPS);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ButtonShowDPS.onClick.RemoveListener(ShowDPS);
    }

    private void ShowDPS()
    {
        Signals.Get<ShowDPSMenu>().Dispatch();
    }

    // private void HideDPS()
    // {
    //     Signals.Get<HideDPSMenu>().Dispatch();
    // }
}
