using Sirenix.OdinInspector;
using SlotHero.Grid;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public UltimateSkillBase UltimateSkill1;
    public UltimateSkillBase UltimateSkill2;
    public CustomGrid combatGrid;

    [Tooltip("This transform use for tracked enemy by calculated distance.")]
    public Transform trackedPosition;

    [Button]
    public void FireAttack()
    {
        UltimateSkill1.Execute(combatGrid);
    }

    [Button]
    public void FreezeAttack()
    {
        UltimateSkill2.Execute(combatGrid);
    }

    private void Start()
    {
        combatGrid = GetComponent<CustomGrid>();
        combatGrid.Init();
    }
}