using CombatSystem.HeroDataManager;
using UnityEngine;

[CreateAssetMenu(fileName = "GetDataSupport", menuName = "GetDataSupport")]
public class GetDataSupport : ScriptableObject
{
    [SerializeField] private StageInformation stageInformation;
    public static GetDataSupport Get()
    {
        return Resources.Load<GetDataSupport>("GetDataSupport");
    }
    public StageInformation StageInformation
    {
        get
        {
            if (stageInformation == null)
            {
                Debug.LogWarning("StageInformation is null");
            }
            return stageInformation;
        }
    }
    [SerializeField] private HeroManager heroManager;
    public HeroManager HeroManager
    {
        get
        {
            if (heroManager == null)
            {
                Debug.LogWarning("StageInformation is null");
            }
            return heroManager;
        }
    }
}
