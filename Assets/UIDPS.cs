using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;

public class UIDPS : APanelController
{
    [SerializeField] private Button ButtonHideUIDPS;

    protected override void AddListeners()
    {
        base.AddListeners();
        ButtonHideUIDPS.onClick.AddListener(HideUIDPS);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ButtonHideUIDPS.onClick.RemoveListener(HideUIDPS);
    }

    private void HideUIDPS()
    {
        Signals.Get<HideDPSMenu>().Dispatch();
    }
}
