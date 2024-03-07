using NewCombat.Entity;
using UnityEngine;

namespace SlotHero
{
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
            SelectionHero.SelectionHero.Instance.OnDragInUI = true;
            SelectionHero.SelectionHero.Instance.heroAttachedInUI = currentHero;
        }

        public void OnUnClicked()
        {
            // TODO: ADD REFERENCE TO SELECTIONHERO
            if (currentHero.InGameSlotIndex != -1) return;
            SelectionHero.SelectionHero.Instance.OnDragInUI = false;
            //SelectionHero.Instance.heroOfUI = null;
            //currentHero.gameObject.SetActive(true);
        }
    }
}