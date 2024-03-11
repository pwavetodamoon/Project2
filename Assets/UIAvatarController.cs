using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.UIFramework;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;
using CombatSystem.Entity;
using LevelAndStats;
using PlayFab_System;
using SlotHero;
using Sirenix.OdinInspector;
using Unity.VisualScripting;


public class UIAvatarController : APanelController
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private HeroCharacter _heroCharacter;
    [SerializeField] private HeroSlotUI _heroSlotUI;
    [Header("Buttons")]
    [SerializeField] private Button _buttonLevelUp;
    [SerializeField] private Button _buttonSkill;
    [SerializeField] private TextMeshProUGUI _cointText;


    [Header("Levels")]
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

    private void Start()
    {
        UpdateCoinText();
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        _buttonLevelUp.onClick.AddListener(OnButtonLevelUpClicked);
        
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        _buttonLevelUp.onClick.RemoveListener(OnButtonLevelUpClicked);
    }
    private void OnButtonLevelUpClicked()
    {
        //if button interact
        _gameManager.UpgradeHeroLevel(_heroCharacter);
    }
    private void UpdateCoinText()
    {
        Debug.Log("check in Updatecointext");
        var data =  _gameManager.GetMoney(_heroCharacter).ToString();
        _cointText.text = data;
        Debug.Log("Data: "+ data);

    }
    public void SetSprite(Sprite newSprite)
    {
        _imageAvatar.sprite = newSprite;
    }

    public void SetHeroCharacter(HeroCharacter heroCharacter)
    {
        _heroCharacter = heroCharacter;
        _heroSlotUI.SetHero(heroCharacter);
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


