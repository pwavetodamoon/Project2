using NewCombat.Entity;
using NewCombat.Entity.Utilities;
using SlotHero;
using UnityEngine;

namespace SelectionHero.Ray
{
    public class RaycastDetectHero : MonoBehaviour
    {
        public HeroCharacter currentHero;
        public Vector2 mousePosition;
        public bool IsHandleHero;
        private AttackManager attackManager;
        private SlotManager slotManager;

        private void Awake()
        {
            slotManager = SlotManager.Instance;
        }

        public void HandleHeroSelection(bool isMouseDown, bool isMouseMove, HeroCharacter _hero)
        {
            if (isMouseDown)
            {
                GetHeroReferences(_hero);
                if (isMouseMove && CannotMoveHero() == false)
                {
                    if (CanHoldHeroInMouse())
                    {
                        MoveHeroByMouse();
                    }
                    else
                    {
                        PutHeroBack();
                        ResetCurrentHeroRef();
                    }
                }
            }
            else
            {
                var swapFinished = SwapHero();
                if (!swapFinished) PutHeroBack();

                ResetCurrentHeroRef();
                IsHandleHero = false;
            }
        }

        private bool CanHoldHeroInMouse()
        {
            return currentHero.EntityInAttackState() == false && attackManager.AttackedByEnemies() == false;
        }

        private bool CannotMoveHero()
        {
            return currentHero == null || attackManager == null || currentHero.IsDead;
        }

        private void GetHeroReferences(HeroCharacter hero)
        {
            if (hero != null && currentHero == null)
            {
                currentHero = hero;
                attackManager = hero.GetComponent<AttackManager>();
            }
        }

        private void MoveHeroByMouse()
        {
            currentHero.transform.position = mousePosition;
            IsHandleHero = true;
        }


        private bool SwapHero()
        {
            if (currentHero != null)
                return slotManager.FindNearSlotAndSwapIfInRange(currentHero, currentHero.InGameSlotIndex);

            return false;
        }

        private void PutHeroBack()
        {
            if (currentHero == null) return;
            slotManager.LoadHeroIntoSlot(currentHero);
        }

        private void ResetCurrentHeroRef()
        {
            currentHero = null;
            attackManager = null;
        }
    }
}