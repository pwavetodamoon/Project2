using System;
using deVoid.UIFramework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemSkill : APanelController
{
    [SerializeField] private Button _buttonSkill;
    [SerializeField] private string _skillName;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _coolDownImage;
    [SerializeField] private TextMeshProUGUI _textCoolDown;
    [SerializeField] private Cooldown _cooldown = new Cooldown();
    [SerializeField] private bool _canUseSkill;
    [SerializeField] private SkillConfig _skillConfig;

    private void Start()
    {
        InitResourceSkill();
    }

    private void FixedUpdate()
    {
        UpdateCooldownUI();
    }

    protected override void AddListeners()
    {
        _buttonSkill.onClick.AddListener(UseSkill);
    }

    protected override void RemoveListeners()
    {
        _buttonSkill.onClick.RemoveListener(UseSkill);
    }

    private void InitResourceSkill()
    {
        _skillName = _skillConfig.GetSkillName();
        _icon.sprite = _skillConfig.GetIconSkill();
        _canUseSkill = _skillConfig.GetIsCanUse();
        _skillConfig.SetImageCoolDown(_coolDownImage);
        _skillConfig.SetCoolDownText(_textCoolDown);
    }

    private void UseSkill()
    {
        if (!_cooldown.IsCoolingDown)
        {
            _cooldown.StartCooldown();
        }
    }

    private void UpdateCooldownUI()
    {
        if (_cooldown.IsCoolingDown)
        {
            _coolDownImage.fillAmount = _cooldown.RemainingCooldownTime / _cooldown.CoolDownTime;
            _textCoolDown.text = Mathf.CeilToInt(_cooldown.RemainingCooldownTime).ToString();
            _coolDownImage.gameObject.SetActive(true);
            _textCoolDown.gameObject.SetActive(true);
            _buttonSkill.interactable = false; // Disable the button during cooldown
          Debug.Log("UpdateCDUI true");
        }
        else
        {
            _coolDownImage.gameObject.SetActive(false);
            _textCoolDown.gameObject.SetActive(false);
           _buttonSkill.interactable = true; // Enable the button when cooldown is finished
           Debug.Log("UpdateCDUI false");

        }
    }
}