using System.Collections;
using System.Collections.Generic;
using NewCombat.Characters;
using UnityEngine;

public class HeroSlotUI : MonoBehaviour
{
    public HeroCharacter currentHero;
    public int SlotIndex;
    public void OnClicked()
    {
        if (currentHero.InGameSlotIndex != -1)
        {
            return;
        }
        currentHero.gameObject.SetActive(true);
        SelectionHero.Instance.OnDragInUI = true;
        SelectionHero.Instance.heroOfUI = currentHero.transform;
    }

    public void OnUnClicked()
    {
        // TODO: ADD REFERENCE TO SELECTIONHERO
        if (currentHero.InGameSlotIndex != -1)
        {
            return;
        }
        SelectionHero.Instance.OnDragInUI = false;
        //SelectionHero.Instance.heroOfUI = null;
        //currentHero.gameObject.SetActive(true);
    }

}
