using System;
using System.Collections;
using System.Collections.Generic;
using CombatSystem.Entity;
using Core.Currency;
using UnityEngine;
using deVoid;
using deVoid.Utils;
using LevelAndStats;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private CurrencyManager _currencyManager;
    private void Awake()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();
        Application.targetFrameRate = 60;
    }

    public void UpgradeHeroLevel(HeroCharacter heroCharacter)
    {
        HeroEntityStats _heroEntityStats = heroCharacter.GetComponent<HeroEntityStats>();
        var moneyRequired =_levelConfig.GetMoneyRequired(_heroEntityStats.Level());
        if (_currencyManager.currency >= Convert.ToInt32(moneyRequired))
        {
            Debug.Log("Upgrade Level");
          _currencyManager.currency -= Convert.ToInt32(moneyRequired);
          _heroEntityStats.Upgrade();
          Signals.Get<SendMoneyLevelRequired>().Dispatch(Convert.ToInt32(_levelConfig.GetMoneyRequired(_heroEntityStats.Level())));
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
    

}
