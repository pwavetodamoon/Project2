using Background;
using CombatSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using CombatSystem.HeroDataManager;
using Core.Reward;
using UnityEngine;
using Helper;
using UI_Effects;
using deVoid.UIFramework;
using deVoid.Utils;
using CombatSystem.Attack.Near;
using UnityEditor.Experimental.GraphView;


public class GameLevelControl : Singleton<GameLevelControl>, ICoroutineRunner
{

    [SerializeField] private HeroManager heroManager;
    [SerializeField] private EnemySpawnerControl enemySpawnerControl;
    [SerializeField] private ScreenTransition screenTransition;
    [SerializeField] private MapBackground mapBackground;
    [SerializeField] private StageInformation stageInformation;


    private LossTransitionHandler LossTransitionHandler;
    private NextMapTransitionHandler NextMapTransitionHandler;
    private void OnValidate()
    {
        if (stageInformation == null)
        {
            stageInformation = GetDataSupport.Get().StageInformation;
        }
        if (heroManager == null)
        {
            heroManager = GetDataSupport.Get().HeroManager;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        LossTransitionHandler = new LossTransitionHandler();
        NextMapTransitionHandler = new NextMapTransitionHandler();

        LossTransitionHandler.GetRef(this, screenTransition, mapBackground);
        NextMapTransitionHandler.GetRef(this, screenTransition, mapBackground);

        LossTransitionHandler.RegisterCallback(StartTransition, EndTransition);
        NextMapTransitionHandler.RegisterCallback(StartTransition, EndTransition);

        stageInformation.OnCompleteMap += OnGoNextMap;

        //enemySpawnerControl.EnableSpawn();
    }
    private void StartTransition()
    {
        enemySpawnerControl.ClearAndStopSpawn();
        RewardManager.Instance.CollectAllItemOnActive();

    }
    private void EndTransition()
    {
        enemySpawnerControl.EnableSpawn();
    }
    public void CheckOnWin()
    {
        enemySpawnerControl.ClearAndStopSpawn();
        
    }
    public void OnWin()
    {
        
    }
    public void CheckOnLoose()
    {
        Debug.Log("Checklose");
        if (CombatEntitiesManager.Instance.GetHeroList().Count > 1) return;
        OnLoose();
    }
    public void OnLoose()
    {
        Signals.Get<OpenLosePanel>().Dispatch();
        LossTransitionHandler.UseRunner();
        stageInformation.ResetStage();
    }

    public void OnGoNextMap()
    {
        NextMapTransitionHandler.UseRunner();
        stageInformation.currentMapIndex++;
        if (stageInformation.currentMapIndex > 1) { }
    }

    private void OnDisable()
    {
        stageInformation.OnCompleteMap -= NextMapTransitionHandler.UseRunner;
        NextMapTransitionHandler.UnRegisterCallback();
        LossTransitionHandler.UnRegisterCallback();
    }

    public void LoadToCurrentMap()
    {
        mapBackground.LoadTexture();
    }
}

public interface OnChangeScreenEvent
{
    void OnChangeScreen();
}