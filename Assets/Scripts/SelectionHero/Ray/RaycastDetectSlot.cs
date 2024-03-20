using SlotHero;
using SlotHero.SlotInGame;
using UnityEngine;

namespace SelectionHero.Ray
{
    public class RaycastDetectSlot : MonoBehaviour
    {
        [SerializeField] private HeroSlotInGame currentSlotInGame;
        public Vector2 mousePosition;
        [SerializeField] private ChestSwap chestSwap;
        public void Detect(bool isMouseDown, bool isContainHero)
        {
            if (isContainHero && isMouseDown &&
                SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out currentSlotInGame))
            {
                currentSlotInGame.SetTriggerShadow();
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
            currentSlotInGame.ResetTriggerShadow();
            currentSlotInGame = null;
        }
    }
}