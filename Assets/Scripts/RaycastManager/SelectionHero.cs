using System.Collections;
using System.Collections.Generic;
using Helper;
using NewCombat.Characters;
using NewCombat.Slots;
using RaycastManager;
using UnityEngine;

public class SelectionHero : Singleton<SelectionHero>
{
    [SerializeField] RaycastDetectHero raycastDetectHero;
    [SerializeField] RaycastDetectSlot raycastDetectSlot;
    [SerializeField] RayInput rayInput;
    public bool OnDragInUI;
    public bool IsMouseMove;
    public HeroCharacter heroOfUI;

    private void Update()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseX != 0 || mouseY != 0)
        {
            IsMouseMove = true;
        }
        else
        {
            IsMouseMove = false;
        }

        rayInput.HandleMouseInput();
        raycastDetectHero.mousePosition = rayInput.worldMousePosition;
        raycastDetectSlot.mousePosition = rayInput.worldMousePosition;
        HeroCharacter hero = null;
        if (OnDragInUI && heroOfUI)
        {
            hero = heroOfUI;
        }
        else if(heroOfUI == null && !OnDragInUI)
        {
            hero = GetHero();
        }

        raycastDetectHero.HandleHeroSelection(rayInput.isMouseDown, IsMouseMove, hero);
        raycastDetectSlot.Detect(rayInput.isMouseDown, IsMouseMove,raycastDetectHero.IsHandleHero);

    }
    public HeroCharacter GetHero()
    {
        var mousePosition = rayInput.worldMousePosition;
        if (SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out HeroSlotInGame newSlotInGame))
        {
            return newSlotInGame.currentHero;
        }
        return null;
    }
}
