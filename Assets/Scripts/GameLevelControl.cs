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


public interface IGameStateHandler
{
    void ChangeAttackStateOfHero(bool state, List<HeroCharacter> heroList);
    void CollectAllItemInGame();
    void ClearMonsterAndStopSpawnOnMap();
}

public class GameLevelControl : Singleton<GameLevelControl>, IGameStateHandler, ICoroutineRunner
{
    [SerializeField] private HeroManager heroManager;
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private ScreenTransition screenTransition;
    [SerializeField] private MapBackground mapBackground;
    [SerializeField] private StageInformation stageInformation;

    private LossTransitionHandler LossTransitionHandler;
    private NextMapTransitionHandler NextMapTransitionHandler;
    protected override void Awake()
    {
        base.Awake();
        LossTransitionHandler = new LossTransitionHandler();
        NextMapTransitionHandler = new NextMapTransitionHandler();

        LossTransitionHandler.GetRef(this, this, screenTransition, mapBackground);
        NextMapTransitionHandler.GetRef(this, this, screenTransition, mapBackground);
        stageInformation.OnCompleteMap += OnGoNextMap;
    }

    public void OnLoose()
    {
        LossTransitionHandler.UseRunner();
        stageInformation.ResetStage();
    }

    public void OnGoNextMap()
    {
        NextMapTransitionHandler.UseRunner();
        stageInformation.currentMapIndex++;
    }

    private void OnDisable()
    {
        stageInformation.OnCompleteMap -= NextMapTransitionHandler.UseRunner;
    }

    public void LoadToMap(int index)
    {
        mapBackground.MapIndex = index;
        mapBackground.LoadTexture();
    }
    public void ChangeAttackStateOfHero(bool state, List<HeroCharacter> heroList)
    {
        foreach (var heroData in heroList)
        {
            heroData.SetAttackState(state);
        }
    }
    public void CollectAllItemInGame()
    {
        RewardManager.Instance.CollectAllItemOnActive();
    }

    [Button]
    public void ClearMonsterAndStopSpawnOnMap()
    {
        monsterSpawner.ClearMonsterAndStopSpawnOnMap();
    }
}

public interface OnChangeScreenEvent
{
    void OnChangeScreen();
}