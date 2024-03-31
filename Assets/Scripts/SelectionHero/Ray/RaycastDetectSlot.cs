using CombatSystem.Entity;
using SlotHero;
using SlotHero.SlotInGame;
using Unity.VisualScripting;
using UnityEngine;

namespace SelectionHero.Ray
{
    public class RaycastDetectSlot : MonoBehaviour
    {
        [SerializeField] private ChestSwap chestSwap;
        private bool doesContainHero;
        private HeroCharacter HeroOnDragInMouse;
        private bool isMouseDown;
        [SerializeField] private HeroSlotInGame SlotIsAboveMouse;
        public Vector2 mousePosition;
        private bool CanFindSlotToSwap() => doesContainHero && isMouseDown;

        private bool IsHeroInSameSlot() => HeroOnDragInMouse.InGameSlotIndex == SlotIsAboveMouse.SlotIndex;

        private void OnCheck()
        {
            if (CanFindSlotToSwap() && OnFindSlot())
            {
                UseVFXWhenTriggerNewHero();
            }
            else if (SlotIsAboveMouse != null)
            {
                SlotIsAboveMouse.StopVFX();
                //SlotIsAboveMouse.ShadowColor.ParticleSystem.Stop();
                SlotIsAboveMouse = null;
            }
        }

        private bool OnFindSlot() => SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out SlotIsAboveMouse);

        private void UseVFXWhenTriggerNewHero()
        {
            if(HeroOnDragInMouse == null || SlotIsAboveMouse == null) return;
            if (IsHeroInSameSlot() == false)
            {
                SlotIsAboveMouse.TriggerVFX();
            }
        }

        public void Detect(bool isMouseDown, bool doesContainHero, HeroCharacter currentHeroOnDrag)
        {
            this.isMouseDown = isMouseDown;
            this.doesContainHero = doesContainHero;
            this.HeroOnDragInMouse = currentHeroOnDrag;
            OnCheck();
        }
    }
}