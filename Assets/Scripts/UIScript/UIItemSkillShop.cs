using System;
using deVoid.UIFramework;
using PlayFab_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSkillShop : APanelController
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameSkill;
    [SerializeField] private TextMeshProUGUI _descriptionSkill;
    [SerializeField] private Button _buttonBuy;
    [SerializeField] private SkillConfig _skillConfig;
    [SerializeField] private int _cost;
    [SerializeField] private UIStartGame _uiStartGame;

    protected override void Awake()
    {
        base.Awake();
        InitResource();
    }

    private void Start()
    {
        _uiStartGame = FindObjectOfType<UIStartGame>();
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        _buttonBuy.onClick.AddListener(Buy);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        _buttonBuy.onClick.RemoveListener(Buy);
    }
    private void InitResource()
    {
        _icon.sprite = _skillConfig.GetIconSkill();
        _nameSkill.text = _skillConfig.GetSkillName();
        _descriptionSkill.text = _skillConfig.GetDesriptTions();
        _cost = _skillConfig.GetCostSkill();
    }

    private void Buy()
    {
        if (PlayFabManager.Instance.Player.gold >= _cost)
        {
            _uiStartGame.RegisterActionEvent(ListenersEventCallBack);
        }
        else
        {
            Debug.Log("can not buy");
        }
    }

    private void ListenersEventCallBack()
    {
        var gold = PlayFabManager.Instance.Player.gold -= _cost;
        _uiStartGame.SetGoldText(gold);
        _uiStartGame.UnregisterActionEvent(ListenersEventCallBack);
        Debug.Log("Gold: "+ gold);
    }
}