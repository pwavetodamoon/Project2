using SlotHero;
using SlotHero.SlotInGame;
using UnityEngine;

namespace SelectionHero.Ray
{
    public class RaycastDetectSlot : MonoBehaviour
    {
        [SerializeField] private HeroSlotInGame currentSlotInGame;
        public Vector2 mousePosition;

        public void Detect(bool isMouseDown, bool isContainHero)
        {
            if (isContainHero && isMouseDown &&
                SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out currentSlotInGame))
            {
                currentSlotInGame.SetTriggerShadow();
                return;
            }

            if (currentSlotInGame != null) ResetCurrentSlot();
        }

        private void ResetCurrentSlot()
        {
            currentSlotInGame.ResetTriggerShadow();
            currentSlotInGame = null;
        }
    }
}