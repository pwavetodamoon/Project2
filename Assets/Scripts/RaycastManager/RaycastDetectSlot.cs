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

    public void Detect(bool isMouseDown,bool isMouseMove ,bool isContainHero)
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
                ResetCurrentSlot(); 
            }
        }
        else
        {
            ResetCurrentSlot();
        }
    }

    private void ResetCurrentSlot()
    {
        if (currentSlotInGame != null)
        {
            Debug.Log("No Set shadow");
            currentSlotInGame.ResetTriggerShadow();
            currentSlotInGame = null;
        }
        newSlotInGame = null;

    }

}
