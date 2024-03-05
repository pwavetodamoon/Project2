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

public class GameStateHandler : Singleton<GameStateHandler>, IGameStateHandler , ICoroutineRunner
{
    [SerializeField] private HeroManager heroManager;
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private ScreenTransition screenTransition;
    [SerializeField] private MapBackground mapBackground;
    
    private IGameTransitionComponents gameTransitionComponents;

    public LossTransitionHandler LossTransitionHandler;
    public NextMapTransitionHandler NextMapTransitionHandler;
    protected override void Awake()
    {
        base.Awake();
        gameTransitionComponents.map = mapBackground;
        gameTransitionComponents.screen = screenTransition;
        gameTransitionComponents.GameStateHandler = this;
        gameTransitionComponents.runner = this;
        Debug.Log($"{gameTransitionComponents.map} {gameTransitionComponents.screen} {gameTransitionComponents.GameStateHandler}");
        LossTransitionHandler = new LossTransitionHandler(gameTransitionComponents);
        NextMapTransitionHandler = new NextMapTransitionHandler(gameTransitionComponents);
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
        foreach (var item in RewardManager.Instance.list)
        {
            if (item.gameObject.activeSelf == false) continue;
            item.Collect();
        }
    }

    [Button]
    public void ClearMonsterAndStopSpawnOnMap()
    {
        var list = monsterSpawner.transform.GetComponentsInChildren<MonsterCharacter>();
        for (int i = 0; i < list.Length;i++)
        {
            list[i].ReleaseObject();
        }
    }
}

public interface OnChangeScreenEvent
{
    void OnChangeScreen();
}