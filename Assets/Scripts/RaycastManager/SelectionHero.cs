using System.Collections;
using System.Collections.Generic;
using Helper;
using NewCombat.Characters;
using NewCombat.Slots;
using RaycastManager;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectionHero : Singleton<SelectionHero>
{
    [SerializeField] RaycastDetectHero raycastDetectHero;
    [SerializeField] RaycastDetectSlot raycastDetectSlot;
    [SerializeField] RayInput rayInput;
    public bool OnDragInUI;
    public HeroCharacter heroAttachedInUI;

    public HeroCharacter currentHeroAttached;
    private void Update()
    {
        UpdateMousePosition();
        raycastDetectHero.HandleHeroSelection(rayInput.isMouseDown, rayInput.isMouseMove, GetHero());
        raycastDetectSlot.Detect(rayInput.isMouseDown, raycastDetectHero.IsHandleHero);
    }

    private HeroCharacter GetHero()
    {

        if (OnDragInUI && heroAttachedInUI)
        {
            currentHeroAttached = heroAttachedInUI;
        }
        else if (heroAttachedInUI == null && !OnDragInUI)
        {
            currentHeroAttached = GetHeroNearMouse();
        }
        else
        {
            currentHeroAttached = null;
        }

        return currentHeroAttached;
    }
    private void UpdateMousePosition()
    {
        rayInput.HandleMouseInput();
        raycastDetectHero.mousePosition = rayInput.worldMousePosition;
        raycastDetectSlot.mousePosition = rayInput.worldMousePosition;
    }
    public HeroCharacter GetHeroNearMouse()
    {
        var mousePosition = rayInput.worldMousePosition;
        if (SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out HeroSlotInGame newSlotInGame))
        {
            return newSlotInGame.currentHero;
        }
        return null;
    }
}
