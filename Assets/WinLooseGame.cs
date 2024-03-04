using Background;
using CombatSystem;
using CombatSystem.Scripts.Spawner;
using DropItem;
using NewCombat.Characters;
using NewCombat.HeroDataManager;
using NewCombat.Slots;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using NewCombat.ManagerInEntity;
using UnityEngine;
using Characters;
using DG.Tweening;

public class WinLooseGame : MonoBehaviour
{
    public HeroManager heroManager;
    public MonsterSpawner monsterSpawner;
    public ScreenTransition screenTransition;
    public MapBackground mapBackground;

    [Button]
    public void GoNextMapSetup()
    {
        StartCoroutine(GoNextMap());
    }

    IEnumerator GoNextMap()
    {
        var entityList = CombatEntitiesManager.Instance.GetHeroList();
        var heroList = new List<HeroCharacter>();


        foreach (var entityCharacter in entityList)
        {
            var hero = entityCharacter.GetComponent<HeroCharacter>();
            heroList.Add(hero);
            if (hero.InGameSlotIndex == -1) continue;
            hero.DOKill();
            hero.StopCurrentAttack();
            hero.SetAttackState(false);
            hero.GetComponent<AnimationManager>().PlayAnimation(Human_Animator.Walk_State);

        }

        foreach (var hero in heroList)
        {
            var model = hero.GetModelTransform();
            model.transform.position = SlotManager.Instance.GetStandTransform(hero.InGameSlotIndex).position;
        }
        // stop all attack;
        ChangeAttackStateOfHero(false, heroList);


        CollectAllItemInGame();

        ClearMonsterAndStopSpawnOnMap();


        yield return screenTransition.StartTransition();
        mapBackground.GoNextMap();
        yield return screenTransition.waitBetweenTransition;
        yield return screenTransition.EndTransition();

        ChangeAttackStateOfHero(true, heroList);


        foreach(var hero in heroList)
        {
            hero.CreateAttack();
        }
    }

    private void ChangeAttackStateOfHero(bool state, List<HeroCharacter> heroList)
    {
        foreach (var heroData in heroList)
        {
            heroData.SetAttackState(state);
        }
    }
    private void CollectAllItemInGame()
    {
        foreach (var item in RewardManager.Instance.list)
        {
            if (item.gameObject.activeSelf == false) continue;
            item.Collect();
        }
    }

    [Button]
    private void ClearMonsterAndStopSpawnOnMap()
    {
        var list = CombatEntitiesManager.Instance.transform.GetComponentsInChildren<MonsterCharacter>();
        for (int i = 0; i < list.Length;i++)
        {
            list[i].ReleaseObject();
        }
    }
}
