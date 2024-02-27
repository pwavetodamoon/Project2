using System.Collections;
using System.Collections.Generic;
using NewCombat.Characters;
using NewCombat.Slots;
using RaycastManager;
using UnityEngine;
using UnityEngine.Serialization;

public class RaycastDetectSlot : MonoBehaviour
{
    [FormerlySerializedAs("currentSlot")] [SerializeField] private HeroSlotInGame currentSlotInGame;
    [FormerlySerializedAs("newSlot")] [SerializeField] private HeroSlotInGame newSlotInGame;
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
            //CharacterSlot newSlot = null;
            //mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (SlotManager.Instance.TryGetSlotNearPosition(mousePosition, out newSlotInGame))
            {
                if (currentSlotInGame == null)
                {
                    currentSlotInGame = newSlotInGame;
                    currentSlotInGame.DesetTriggerShadow();

                }
            }
            else
            {
                ResetCurrentSlot();
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
            currentSlotInGame.SetTriggerShadow();
            currentSlotInGame = null;
        }
    }
    
}
