using NewCombat.Characters;
using UnityEngine;

public class HeroSlotUI : MonoBehaviour
{
    [SerializeField] private HeroCharacter currentHero;

    public void SetHero(HeroCharacter hero)
    {
        currentHero = hero;
    }

    public void OnClicked()
    {
        if (currentHero.InGameSlotIndex != -1) return;
        currentHero.gameObject.SetActive(true);
        SelectionHero.Instance.OnDragInUI = true;
        SelectionHero.Instance.heroAttachedInUI = currentHero;
    }

    public void OnUnClicked()
    {
        // TODO: ADD REFERENCE TO SELECTIONHERO
        if (currentHero.InGameSlotIndex != -1) return;
        SelectionHero.Instance.OnDragInUI = false;
        //SelectionHero.Instance.heroOfUI = null;
        //currentHero.gameObject.SetActive(true);
    }
}