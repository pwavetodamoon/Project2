using System.Collections;
using NewCombat.Slots;
using Sirenix.OdinInspector;

public class LossTransitionHandler
{

    private IGameTransitionComponents gameTransitionComponents;
    public LossTransitionHandler(IGameTransitionComponents gameTransitionComponents)
    {
        this.gameTransitionComponents = gameTransitionComponents;
    }

    public void StartCoroutine()
    {
        gameTransitionComponents.runner.StartCoroutine(OnLooseProgress());
    }

    private IEnumerator OnLooseProgress()
    {
        var slotList = SlotManager.Instance.Slots;

        gameTransitionComponents.GameStateHandler.CollectAllItemInGame();

        gameTransitionComponents.GameStateHandler.ClearMonsterAndStopSpawnOnMap();
        // Health all hero in slot

        yield return gameTransitionComponents.screen.StartTransition();
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].currentHero != null && slotList[i].currentHero.IsDead)
            {
                var stats = slotList[i].currentHero.GetComponent<HeroEntityStats>();
                stats.IncreaseHealth(stats.MaxHealth());
                slotList[i].currentHero.RegisterObject();
            }
        }

        gameTransitionComponents.map.GoNextMap();
        yield return gameTransitionComponents.screen.waitBetweenTransition;
        yield return gameTransitionComponents.screen.EndTransition();
    }
}
