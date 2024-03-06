using Background;
using CombatSystem;
using CombatSystem.Scripts.Spawner;
using DropItem;
using NewCombat.Characters;
using NewCombat.HeroDataManager;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using NewCombat;


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


    public LossTransitionHandler LossTransitionHandler;
    public NextMapTransitionHandler NextMapTransitionHandler;
    protected override void Awake()
    {
        base.Awake();
        LossTransitionHandler = new LossTransitionHandler();
        NextMapTransitionHandler = new NextMapTransitionHandler();

        LossTransitionHandler.GetRef(this, this, screenTransition, mapBackground);
        NextMapTransitionHandler.GetRef(this, this, screenTransition, mapBackground);
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