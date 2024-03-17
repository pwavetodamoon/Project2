using System;
using System.Collections;
using System.Collections.Generic;
using CombatSystem.HeroDataManager;
using CombatSystem.HeroDataManager.Data;
using Helper;
using Sirenix.OdinInspector;
using UnityEngine;

public class GetScriptableObjectSupport : Singleton<GetScriptableObjectSupport>
{
    [SerializeField] private StageInformation stageInformation;
    public StageInformation StageInformation
    {
        get
        {
            if (stageInformation == null)
            {
                Debug.LogWarning("StageInformation is null");
            }
            return stageInformation;
        }
    }
    [SerializeField] private HeroManager heroManager;
    public HeroManager HeroManager
    {
        get
        {
            if (heroManager == null)
            {
                Debug.LogWarning("StageInformation is null");
            }
            return heroManager;
        }
    }
}
