using NewCombat.Characters;
using NewCombat.ManagerInEntity;
using NewCombat.Slots;
using Unity.VisualScripting;
using UnityEngine;

namespace RaycastManager
{
    public class RaycastDetectHero : MonoBehaviour
    {
        public HeroCharacter currentHero;
        private AttackManager attackManager;
        public Vector2 mousePosition;
        public bool IsHandleHero = false;

        public void HandleHeroSelection(bool isMouseDown, bool isMouseMove, HeroCharacter _hero)
        {
            //ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (isMouseDown)
            {
                HandleMouseDown(_hero);
                if (isMouseMove)
                {
                    HandleHeroMovement();
                }
            }
            else
            {
                HandleMouseUp();
            }
        }
        private void HandleMouseDown(HeroCharacter _hero)
        {
            if (_hero != null && currentHero == null)
            {
                currentHero = _hero;
                attackManager = _hero.GetComponent<AttackManager>();
            }
        }
        private void HandleHeroMovement()
        {
            if (currentHero == null || attackManager == null || currentHero.IsDead) return;
            
            // If cannot hold hero then put it back to the slot
            bool canHoldHero = currentHero.EntityInAttackState() == false && attackManager.AttackedByEnemies() == false;
            if (canHoldHero)
            {
                currentHero.transform.position = new Vector2(mousePosition.x, mousePosition.y);
                IsHandleHero = true;
            }
            else
            {
                SlotManager.Instance.LoadHeroIntoSlot(currentHero);
                currentHero = null;
                IsHandleHero = false;
            }
        }

        private void HandleMouseUp()
        {
            if (currentHero != null)
            {
                var isSwap = SlotManager.Instance.FindNearSlotAndSwapIfInRange(currentHero, currentHero.InGameSlotIndex);
                if (!isSwap)
                {
                    SlotManager.Instance.LoadHeroIntoSlot(currentHero);
                }
                currentHero = null;
                attackManager = null;
            }
            IsHandleHero = false;
        }


    }
}
