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
    [SerializeField] private HeroSlotInGame newSlotInGame;
    public Vector2 mousePosition;

    public void Detect(bool isMouseDown, bool isContainHero)
    {
        //allowRay = raycastDetectHero.IsMouseDown();
        if (isContainHero == false)
        {
            ResetCurrentSlot();
            newSlotInGame = null;
            return;
        }
        if (isMouseDown)
        {
            if (SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out newSlotInGame))
            {
                if (currentSlotInGame == null)
                {
                    currentSlotInGame = newSlotInGame;
                    currentSlotInGame.SetTriggerShadow();
                    Debug.Log("Set shadow");
                }
                Debug.Log("Find shadow");
            }
            else
            {
                ResetCurrentSlot(); Debug.Log("No Set shadow");
            }
        }
        else
        {
            ResetCurrentSlot();
            newSlotInGame = null;
        }
    }

    private void ResetCurrentSlot()
    {
        if (currentSlotInGame != null)
        {
            currentSlotInGame.ResetTriggerShadow();
            currentSlotInGame = null;
        }
    }
    
}
