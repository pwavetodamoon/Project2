using System.Collections;
using System.Collections.Generic;
using CombatSystem;
using NewCombat.Characters;
using UnityEngine;
using Helper;
using NewCombat.Slots;
using Sirenix.OdinInspector;

public class GameProgressControl : MonoBehaviour
{
    [Button]
    public void Test()
    {
        var entities = FindObjectsOfType<EntityCharacter>();
        foreach (var entity in entities)
        {
            entity.ReleaseObject();
        }
    }

}
