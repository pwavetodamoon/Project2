using System.Collections;
using LevelAndStats;
using Sirenix.OdinInspector;
using SlotHero;
using UnityEngine;

public class LossTransitionHandler : GameTransitionBase
{
    private YieldInstruction WaitTime = new WaitForSeconds(0.5f);
    public override void UseRunner()
    {
        runner.StartCoroutine(OnLooseProgress());
    }

    private IEnumerator OnLooseProgress()
    {
        OnStartTransition?.Invoke();
        var slotList = SlotManager.Instance.Slots;


        // Health all hero in slot
        yield return WaitTime;

        yield return screen.StartTransition();
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].currentHero != null && slotList[i].currentHero.IsDead)
            {
                var stats = slotList[i].currentHero.GetComponent<HeroEntityStats>();
                stats.IncreaseHealth(stats.MaxHealth());
                slotList[i].currentHero.RegisterObject();
            }
        }

        map.GoNextMap();
        yield return screen.waitBetweenTransition;
        yield return screen.EndTransition();
        OnTransitionEnd?.Invoke();
    }
}
