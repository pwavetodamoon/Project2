using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.UIFramework;
using UnityEngine.UI;
using TMPro;
using System;


public class UIAvatarController : APanelController
{
    [SerializeField] private CharacterEnumType _characterType;

    [Header("Buttons")]
    [SerializeField] private Button _buttonLevelUp;
    [SerializeField] private Button _buttonSkill;

    [Header("Levels")]
    [SerializeField] private Slider _sliderLevel;
    [SerializeField] private TextMeshProUGUI _textLevel;

    [Header("Avatar")]
    [SerializeField] private Image _imageAvatar;
    [SerializeField] private List<Sprite> _spriteAvatar;


    protected override void Awake()
    {
        base.Awake();
        ChangeSpriteAvatar();
    }
    protected override void AddListeners()
    {
        base.AddListeners();
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
    }
    private void ChangeSpriteAvatar()
    {
        switch (_characterType)
        {
            case CharacterEnumType.King:
                {
                    _imageAvatar.sprite = _spriteAvatar[Convert.ToInt32(_characterType)];
                    break;
                }
            case CharacterEnumType.Knight:
                {
                    _imageAvatar.sprite = _spriteAvatar[Convert.ToInt32(_characterType)];
                    break;
                }
            case CharacterEnumType.Sergeant:
                {
                    _imageAvatar.sprite = _spriteAvatar[Convert.ToInt32(_characterType)];
                    break;
                }
            case CharacterEnumType.Templar:
                {
                    _imageAvatar.sprite = _spriteAvatar[Convert.ToInt32(_characterType)];
                    break;
                }
            case CharacterEnumType.WhiteArmored:
                {
                    _imageAvatar.sprite = _spriteAvatar[Convert.ToInt32(_characterType)];
                    break;
                }
        }
    }




}
