using Helper;
using NewCombat.Characters;
using NewCombat.Slots;
using RaycastManager;
using UnityEngine;

public class SelectionHero : Singleton<SelectionHero>
{
    [SerializeField] private RaycastDetectHero raycastDetectHero;
    [SerializeField] private RaycastDetectSlot raycastDetectSlot;
    [SerializeField] private RayInput rayInput;
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
            currentHeroAttached = heroAttachedInUI;
        else if (heroAttachedInUI == null && !OnDragInUI)
            currentHeroAttached = GetHeroNearMouse();
        else
            currentHeroAttached = null;

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
        if (SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out var newSlotInGame))
            return newSlotInGame.currentHero;
        return null;
    }
}