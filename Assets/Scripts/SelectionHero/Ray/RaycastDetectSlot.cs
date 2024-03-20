using CombatSystem.Entity;
using SlotHero;
using SlotHero.SlotInGame;
using UnityEngine;

namespace SelectionHero.Ray
{
    public class RaycastDetectSlot : MonoBehaviour
    {
        [SerializeField] private HeroSlotInGame currentSlotInGame;
        public Vector2 mousePosition;
        private HeroCharacter currentHero;
        [SerializeField] private ChestSwap chestSwap;
        public void Detect(bool isMouseDown, bool isContainHero, HeroCharacter currentHero)
        {
            if (isContainHero && isMouseDown &&
                SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out currentSlotInGame))
            {
                this.currentHero = currentHero;
                if (currentHero.InGameSlotIndex != currentSlotInGame.SlotIndex) currentSlotInGame.SetTriggerShadow();
                if (currentSlotInGame.SlotIndex == -1)
                {
                    chestSwap.Trigger();
                }
                return;
            }

            if (currentSlotInGame != null) ResetCurrentSlot();
        }

        private void ResetCurrentSlot()
        {
            chestSwap.Reset();
            if (currentHero != null && currentHero.InGameSlotIndex != currentSlotInGame.SlotIndex)
            {
            }
            currentSlotInGame.ResetTriggerShadow();

            currentSlotInGame = null;
        }
    }
}