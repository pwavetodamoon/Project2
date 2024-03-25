using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : APanelController
{
    [SerializeField] private Button _buttonClose;
    [SerializeField] private TextMeshProUGUI _goldText;

    protected override void AddListeners()
    {
        base.AddListeners();

    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
    }
}
