using NewCombat.Characters;
using NewCombat.ManagerInEntity;
using NewCombat.Slots;
using Unity.VisualScripting;
using UnityEngine;

namespace RaycastManager
{
    public class RaycastDetectHero : MonoBehaviour
    {
        Camera mainCamera;
        public LayerMask layerMask;
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public HeroCharacter currentHero;
        private AttackManager attackManager;
        public Vector2 mousePosition;
        public bool IsHandleHero = false;

        public void HandleHeroSelection(bool isMouseDown, Transform _hero)
        {
            //ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (isMouseDown)
            {
                HandleMouseDown(_hero);
            }
            else
            {
                HandleMouseUp();
            }
        }
        private void HandleMouseDown(Transform _hero)
        {
            if (_hero != null && currentHero == null)
            {
                currentHero = _hero.GetComponent<HeroCharacter>();
                attackManager = _hero.GetComponent<AttackManager>();
            }
            else if (currentHero != null)
            {
                HandleHeroMovement();
            }
        }
        private void HandleHeroMovement()
        {
            bool canHoldHero = currentHero.EntityInAttackState() == false && attackManager.AttackedByEnemies() == false;
            if (canHoldHero)
            {
                currentHero.transform.position = new Vector2(mousePosition.x, mousePosition.y);
                IsHandleHero = true;
            }
            else
            {
                SlotManager.Instance.SetHeroBackToOriginalSlot(currentHero, currentHero.InGameSlotIndex);
                currentHero = null;
                IsHandleHero = false;
            }
        }

        private void HandleMouseUp()
        {
            if (currentHero != null)
            {
                // TODO: Can check if first selected game object is game avatar then can swap in to banned slot
                var isInRange = SlotManager.Instance.FindNearSlotAndSwapIfInRange(currentHero, currentHero.InGameSlotIndex);
                if (!isInRange)
                {
                    SlotManager.Instance.SetHeroBackToOriginalSlot(currentHero, currentHero.InGameSlotIndex);
                }
                currentHero = null;
                attackManager = null;
            }
            IsHandleHero = false;
        }


    }
}
