using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.UIFramework;
using UnityEngine.UI;
using TMPro;
using System;
using CombatSystem.Entity;
using deVoid.Utils;
using PlayFab_System;
using Sirenix.OdinInspector;
using SlotHero;


public class UIAvatarController : APanelController
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private HeroCharacter _heroCharacter;
    [SerializeField] private HeroSlotUI _heroSlotUI;
    [Header("Buttons")]
    [SerializeField] private Button _buttonSkill;

    [Header("Levels")]
    [SerializeField] private Button _buttonLevelUp;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private Slider _sliderLevel;
    [SerializeField] private TextMeshProUGUI _textLevel;

    [Header("Avatar")]
    [SerializeField] private Image _imageAvatar;
    [SerializeField] private List<Sprite> _spriteAvatar;
    public int index = 0;

    protected override void Awake()
    {
        base.Awake();
        _heroSlotUI = GetComponent<HeroSlotUI>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    protected override void AddListeners()
    {
        base.AddListeners();
        _buttonLevelUp.onClick.AddListener(OnButtonLevelUpClicked);
        Signals.Get<SendMoneyLevelRequired>().AddListener(SetMoneyRequired);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        _buttonLevelUp.onClick.RemoveListener(OnButtonLevelUpClicked);
        Signals.Get<SendMoneyLevelRequired>().RemoveListener(SetMoneyRequired);

    }
    private void OnButtonLevelUpClicked()
    {
        //if button interact
        _gameManager.UpgradeHeroLevel(_heroCharacter);
    }

    private void SetMoneyRequired(int value)
    {
        _coinText.text = value.ToString();
        Debug.Log("Click and money : " + _textLevel.text);
    }
    public void SetSprite(Sprite newSprite)
    {
        _imageAvatar.sprite = newSprite;
    }

    public void SetHeroCharacter(HeroCharacter heroCharacter)
    {
        _heroCharacter = heroCharacter;
        _heroSlotUI.SetHero(heroCharacter);
        _coinText.text = _gameManager.GetMoneyLevelRequired(_heroCharacter).ToString();
    }
    

    public HeroCharacter GetHeroCharacter()
    {
        return _heroCharacter;
    }
    // private void ChangeSpriteAvatar()
    // {
    //     switch (_heroCharacter.characterEnumType)
    //     {
    //         case CharacterEnumType.King:
    //             {
    //                 _imageAvatar.sprite = _spriteAvatar[Convert.ToInt32(_heroCharacter.characterEnumType)];
    //                 break;
    //             }
    //         case CharacterEnumType.Knight:
    //             {
    //                 _imageAvatar.sprite = _spriteAvatar[Convert.ToInt32(_heroCharacter.characterEnumType)];
    //                 break;
    //             }
    //         case CharacterEnumType.Sergeant:
    //             {
    //                 _imageAvatar.sprite = _spriteAvatar[Convert.ToInt32(_heroCharacter.characterEnumType)];
    //                 break;
    //             }
    //         case CharacterEnumType.Templar:
    //             {
    //                 _imageAvatar.sprite = _spriteAvatar[Convert.ToInt32(_heroCharacter.characterEnumType)];
    //                 break;
    //             }
    //         case CharacterEnumType.WhiteArmored:
    //             {
    //                 _imageAvatar.sprite = _spriteAvatar[Convert.ToInt32(_heroCharacter.characterEnumType)];
    //                 break;
    //             }
    //     }
}


