using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public UltimateSkill UltimateSkill1;
    [Tooltip("This transform use for tracked enemy by calculated distance.")]
    public Transform trackedPosition;
    [Button]
    public void FireAttack()
    {
        UltimateSkill1.Execute(trackedPosition);
    }
}
