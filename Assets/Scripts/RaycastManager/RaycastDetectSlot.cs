using System.Collections;
using System.Collections.Generic;
using NewCombat.Characters;
using NewCombat.Slots;
using RaycastManager;
using UnityEngine;
using UnityEngine.Serialization;

public class RaycastDetectSlot : MonoBehaviour
{
    [SerializeField] private HeroSlotInGame currentSlotInGame;
    public Vector2 mousePosition;

    public void Detect(bool isMouseDown, bool isContainHero)
    {
        if (isContainHero && isMouseDown && SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out currentSlotInGame))
        {
            currentSlotInGame.SetTriggerShadow();
            return;
        }

        if (currentSlotInGame != null)
        {
            ResetCurrentSlot();
        }
    }

    private void ResetCurrentSlot()
    {
        currentSlotInGame.ResetTriggerShadow();
        currentSlotInGame = null;
    }

}
