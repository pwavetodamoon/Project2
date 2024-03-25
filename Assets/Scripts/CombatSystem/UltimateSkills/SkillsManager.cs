using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public UltimateSkill UltimateSkill1;
    [Button]
    public void FireAttack()
    {
        UltimateSkill1.Execute();
    }
}
