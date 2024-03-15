using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;

public class UILoginController : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameLogin;
    
    private void Awake()
    {
        _uIFrameLogin = _defaultUISetting.CreateUIInstance();
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

    }
    private void RemoveListener()
    {
 
    }
}
