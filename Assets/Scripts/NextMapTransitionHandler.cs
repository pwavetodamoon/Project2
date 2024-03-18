using System;
using System.Collections;
using System.Collections.Generic;
using Background;
using CombatSystem;
using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using DG.Tweening;
using Model.Hero;
using Model.Monsters;
using UI_Effects;


public abstract class GameTransitionBase
{
    public void GetRef(ICoroutineRunner runner, ScreenTransition screen,
        MapBackground map)
    {
        this.runner = runner;
        this.screen = screen;
        this.map = map;
    }
    public void RegisterCallback(Action OnStartTransition, Action OnTransitionEnd)
    {
        this.OnStartTransition += OnStartTransition;
        this.OnTransitionEnd += OnTransitionEnd;
    }
    public void UnRegisterCallback()
    {
        this.OnStartTransition = null;
        this.OnTransitionEnd = null;
    }

    protected ICoroutineRunner runner;
    protected ScreenTransition screen;
    protected MapBackground map;

    public abstract void UseRunner();
    public Action OnStartTransition;

    public Action OnTransitionEnd;
    protected virtual void ChangeAttackStateOfHero(bool state, List<HeroCharacter> heroList)
    {
        foreach (var heroData in heroList)
        {
            heroData.SetAttackState(state);
        }
    }
}
public class NextMapTransitionHandler : GameTransitionBase
{
    public override void UseRunner()
    {
        runner.StartCoroutine(GoNextMap());
    }

    private IEnumerator GoNextMap()
    {
        OnStartTransition?.Invoke();


        var entityList = CombatEntitiesManager.Instance.GetHeroList();
        var heroList = new List<HeroCharacter>();


        for (int i = 0; i < entityList.Count; i++)
        {
            var hero = entityList[i].GetComponent<HeroCharacter>();
            heroList.Add(hero);
            if (hero.InGameSlotIndex == -1) continue;
            hero.DOKill();
            hero.ReleaseObject();
            hero.GetComponentInChildren<Animator_Base>().ChangeAnimation(AnimationType.Walk);

        }
        foreach (var hero in heroList)
        {
            hero.SetModelBackImmediate();
        }
        // stop all attack;
        ChangeAttackStateOfHero(false, heroList);


        yield return screen.StartTransition();
        map.GoNextMap();
        yield return screen.waitBetweenTransition;
        yield return screen.EndTransition();

        ChangeAttackStateOfHero(true, heroList);


        foreach (var hero in heroList)
        {
            hero.RegisterObject();
        }

        OnTransitionEnd?.Invoke();
    }
}