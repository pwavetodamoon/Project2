using CombatSystem.Entity;
using SlotHero;
using SlotHero.SlotInGame;
using UnityEngine;

namespace SelectionHero.Ray
{
    public class RaycastDetectSlot : MonoBehaviour
    {
        [SerializeField] private HeroSlotInGame SlotIsAboveMouse;
        [SerializeField] private ChestSwap chestSwap;
        public Vector2 mousePosition;

        public void Detect(bool isMouseDown, bool doesContainHero, HeroCharacter currentHeroOnDrag)
        {
            if (doesContainHero && isMouseDown && SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out SlotIsAboveMouse))
            {
                if (SlotIsAboveMouse.SlotIndex == -1)
                {
                    chestSwap?.Trigger();
                }

                bool doesHeroOnDragDifferHeroFromSlot = currentHeroOnDrag != null &&
                currentHeroOnDrag.InGameSlotIndex != SlotIsAboveMouse.SlotIndex;
                if (doesHeroOnDragDifferHeroFromSlot)
                {
                    SlotIsAboveMouse.SetTriggerShadow();
                }
            }
            else if (SlotIsAboveMouse != null)
            {
                SlotIsAboveMouse.ResetTriggerShadow();
                SlotIsAboveMouse = null;
            }
        }

    }
}