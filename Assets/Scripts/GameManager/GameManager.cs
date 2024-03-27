using System;
using System.Collections;
using System.Collections.Generic;
using CombatSystem.Entity;
using Core.Currency;
using Effects.Skill;
using UnityEngine;
using LevelAndStats;
using Sirenix.OdinInspector;
using SlotHero.SlotInGame;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private CurrencyManager _currencyManager;
    [SerializeField] private GameObject _slotManager;
    [SerializeField] private ParticalSystemsManager _particalSystems;
    [SerializeField]  private List<HeroSlotInGame> heroSlotInGames = new List<HeroSlotInGame>();
    [SerializeField] private SkillsManager _skillsManager;
    private void Awake()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();
        _skillsManager = FindObjectOfType<SkillsManager>();
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        GetAllHeroesCharacterInScenes();
    }

    public void UpgradeHeroLevel(HeroCharacter heroCharacter , TextMeshProUGUI textMeshProUGUI)
    {
        HeroEntityStats _heroEntityStats = heroCharacter.GetComponent<HeroEntityStats>();
        var moneyRequired =_levelConfig.GetMoneyRequired(_heroEntityStats.Level());
        if (_currencyManager.currency >= Convert.ToInt32(moneyRequired))
        {
            _currencyManager.currency -= Convert.ToInt32(moneyRequired);
            heroCharacter.GetComponent<ParticalSystemsManager>().FindAndPlayEffect(EffectSkillsEnum.UpgradeEffect);
            _heroEntityStats.Upgrade();
            textMeshProUGUI.text = _levelConfig.GetMoneyRequired(_heroEntityStats.Level()).ToString();
        }
        else
        {
            Debug.Log("Can not Upgrade");
            Debug.Log("moneyRequired:" + moneyRequired);
        }
    }

    public int GetMoneyLevelRequired(HeroCharacter heroCharacter)
    {
        HeroEntityStats _heroEntityStats = heroCharacter.GetComponent<HeroEntityStats>();
        return Convert.ToInt32( _levelConfig.GetMoneyRequired(_heroEntityStats.Level()));
    }
    public void OnSkillUse(SkillEnum skillEnum )
    {
        switch (skillEnum)
        {
            case SkillEnum.HealTeamSkill:
                HealTeamSkill();
                break;
            case SkillEnum.FireBall:
                FireBallSkill();
                break;
            case SkillEnum.Freeze:
                FreezeSkill();
                break;
        }
      
    }
    private void HealTeamSkill()
    {    float healValue = 50f;
        heroSlotInGames.ForEach(item => item.currentHero.entityStateManager.EntityStats.IncreaseHealth(healValue));
        PlayEffectSkill(EffectSkillsEnum.HealthEffect, EffectSkillsEnum.HealthTeamEffect);
    }
    private void FireBallSkill()
    {
        _skillsManager.FireAttack();
        PlayEffectSkill(EffectSkillsEnum.FireBallEffect, EffectSkillsEnum.FireTeamEffect);

    }
    private void FreezeSkill()
    {   
        _skillsManager.FreezeAttack();
        PlayEffectSkill(EffectSkillsEnum.FreezeEffect, EffectSkillsEnum.FreezeTeamEffect);
    }

    private void PlayEffectSkill(EffectSkillsEnum effectSkills,EffectSkillsEnum effectSkillsTeam)
    {
        foreach (var hero in heroSlotInGames)
        { 
            hero.currentHero.GetComponent<ParticalSystemsManager>().FindAndPlayEffect(effectSkills);
        }
        _particalSystems.FindAndPlayEffect(effectSkillsTeam);
    }
    private void GetAllHeroesCharacterInScenes()
    {
        HeroSlotInGame[] heroSlots = _slotManager.GetComponentsInChildren<HeroSlotInGame>();
        heroSlotInGames.Clear();
        for (int i = 0; i < heroSlots.Length ; i++)
        {
            if (heroSlots[i].SlotIndex == -1)
            {
                continue;
            }
            heroSlotInGames.Add(heroSlots[i]);
        }
    }
    
}
