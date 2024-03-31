using System;
using System.Collections;
using System.Collections.Generic;
using CombatSystem.Entity;
using Helper;
using Sirenix.OdinInspector;
using SlotHero;
using UnityEngine;
public class HealthBarManager : Singleton<HealthBarManager>
{
    public List<HealthBarStatic> healthBars;
    public List<HealthBarStatic> MonsterHealthBars;
    public HealthBarDynamic healthBarPrefab;
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
            slot.SetHealthBar(healthBar);
        }
    }
    public HealthBarDynamic GetHealthBars(EntityCharacter entity)
    {
        if (healthBarPrefab == null) return null;
        var healthBar = Instantiate(healthBarPrefab, transform);
        healthBar.SetTarget(entity);
        return healthBar;
    }
}
