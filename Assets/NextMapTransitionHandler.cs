using System.Collections;
using System.Collections.Generic;
using Background;
using Characters;
using CombatSystem;
using DG.Tweening;
using NewCombat;
using NewCombat.Characters;
using NewCombat.ManagerInEntity;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine;


public abstract class GameTransitionBase
{
    public void GetRef(ICoroutineRunner runner, IGameStateHandler GameStateHandler, ScreenTransition screen,
        MapBackground map)
    {
        this.runner = runner;
        this.screen = screen;
        this.map = map;
        this.GameStateHandler = GameStateHandler;
    }
    protected ICoroutineRunner runner;
    protected IGameStateHandler GameStateHandler;
    protected ScreenTransition screen;
    protected MapBackground map;

    public abstract void UseRunner();
}
public class NextMapTransitionHandler : GameTransitionBase
{
    public override void UseRunner()
    {
        runner.StartCoroutine(GoNextMap());
    }

    private IEnumerator GoNextMap()
    {
        var entityList = CombatEntitiesManager.Instance.GetHeroList();
        var heroList = new List<HeroCharacter>();


        for (int i = 0; i < entityList.Count; i++)
        {
            var hero = entityList[i].GetComponent<HeroCharacter>();
            heroList.Add(hero);
            if (hero.InGameSlotIndex == -1) continue;
            hero.DOKill();
            hero.ReleaseObject();
            hero.GetComponent<AnimationManager>().PlayAnimation(Human_Animator.Walk_State);

        }
        foreach (var hero in heroList)
        {
            hero.SetModelBackImmediate();
        }
        // stop all attack;
        GameStateHandler.ChangeAttackStateOfHero(false, heroList);


        GameStateHandler.CollectAllItemInGame();

        GameStateHandler.ClearMonsterAndStopSpawnOnMap();


        yield return screen.StartTransition();
        map.GoNextMap();
        yield return screen.waitBetweenTransition;
        yield return screen.EndTransition();

        GameStateHandler.ChangeAttackStateOfHero(true, heroList);


        foreach(var hero in heroList)
        {
            hero.RegisterObject();
        }
    }
}