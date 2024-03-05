using System.Collections;
using NewCombat.Slots;
using Sirenix.OdinInspector;

public class LossTransitionHandler : GameTransitionBase
{

    public override void UseRunner()
    {
        runner.StartCoroutine(OnLooseProgress());
    }

    private IEnumerator OnLooseProgress()
    {
        var slotList = SlotManager.Instance.Slots;

        GameStateHandler.CollectAllItemInGame();

        GameStateHandler.ClearMonsterAndStopSpawnOnMap();
        // Health all hero in slot

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
    }
}
