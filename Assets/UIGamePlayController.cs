using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;

public class UIGamePlayController : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameGamePlay;
    private void Awake()
    {
        _uIFrameGamePlay = _defaultUISetting.CreateUIInstance();
    }
    private void Start()
    {
        AddListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }
    private void AddListener()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UIGamePlay);
        Signals.Get<ShowDPSMenu>().AddListener(ShowUIDPS);
        Signals.Get<HideDPSMenu>().AddListener(HideUIDPS);
    }

    private void RemoveListener()
    {
        Signals.Get<HideDPSMenu>().RemoveListener(HideUIDPS);
        Signals.Get<ShowDPSMenu>().RemoveListener(ShowUIDPS);
    }

    private void ShowUIDPS()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UIDPS);
    }
    private void HideUIDPS()
    {
        _uIFrameGamePlay.HidePanel(ScreenIds.UIDPS);
    }
}
