using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using SlotHero;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public List<HealthBar> healthBars;
    private void Start()
    {
        SetHealthBarPosition();
    }
    [Button]
    private void SetHealthBarPosition()
    {
        // Use for edit only
        for (int i = 0; i < SlotManager.Instance.Slots.Count; i++)
        {
            if (i >= healthBars.Count)
            {
                break;
            }
            var slot = SlotManager.Instance.Slots[i];
            var healthBar = healthBars[i];
            healthBar.SetPosition(slot.GetCharacterPosition().position);
            slot.healthBar = healthBar;
        }
    }

}
