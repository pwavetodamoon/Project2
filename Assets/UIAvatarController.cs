using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.UIFramework;
using UnityEngine.UI;
using TMPro;
using System;
using CombatSystem.Entity;
using SlotHero;


public class UIAvatarController : APanelController
{
    [SerializeField] private HeroCharacter _heroCharacter;
    [SerializeField] private HeroSlotUI _heroSlotUI;
    [Header("Buttons")]
    [SerializeField] private Button _buttonLevelUp;
    [SerializeField] private Button _buttonSkill;

    [Header("Levels")]
    [SerializeField] private Slider _sliderLevel;
    [SerializeField] private TextMeshProUGUI _textLevel;

    [Header("Avatar")]
    [SerializeField] private Image _imageAvatar;
    [SerializeField] private List<Sprite> _spriteAvatar;
    public int index = 0;
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

    protected override void Awake()
    {
        base.Awake();
        _heroSlotUI = GetComponent<HeroSlotUI>();
    }
    protected override void AddListeners()
    {
        base.AddListeners();
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
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


