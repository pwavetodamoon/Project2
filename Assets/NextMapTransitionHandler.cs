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

public interface IGameTransitionComponents
{
    ICoroutineRunner runner { get; set; }
    IGameStateHandler GameStateHandler { get; set; }
    ScreenTransition screen { get; set; }
    MapBackground map { get; set; }
}
public class NextMapTransitionHandler
{

    public NextMapTransitionHandler(IGameTransitionComponents gameTransitionComponents)
    {
        this.gameTransitionComponents = gameTransitionComponents;
    }

    private IGameTransitionComponents gameTransitionComponents;
    [Button]
    public void GoNextMapSetup()
    {
        gameTransitionComponents.runner.StartCoroutine(GoNextMap());
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
        gameTransitionComponents.GameStateHandler.ChangeAttackStateOfHero(false, heroList);


        gameTransitionComponents.GameStateHandler.CollectAllItemInGame();

        gameTransitionComponents.GameStateHandler.ClearMonsterAndStopSpawnOnMap();


        yield return gameTransitionComponents.screen.StartTransition();
        gameTransitionComponents.map.GoNextMap();
        yield return gameTransitionComponents.screen.waitBetweenTransition;
        yield return gameTransitionComponents.screen.EndTransition();

        gameTransitionComponents.GameStateHandler.ChangeAttackStateOfHero(true, heroList);


        foreach(var hero in heroList)
        {
            hero.RegisterObject();
        }
    }
}