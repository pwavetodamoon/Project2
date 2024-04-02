using System;
using Core.Quest;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "StageInformation", menuName = "ScriptableObjects/StageInformation")]
public class StageInformation : ScriptableObject
{
    [SerializeField] private MapGameConfig[] MapGameConfigs;
    public int pointsPerMonsterKill;
    public int pointCollected;
    public int currentMapIndex;
    public int currentStageIndex;

    public event Action OnCompleteMap;
    public event Action OnCompleteStage;
    private MapGameConfig currentMapGameData => MapGameConfigs[currentMapIndex];

    public bool allowGoNextMap;
    public bool allowGoNextStage;
    StageSupport stageSupport = new StageSupport();

    /// <summary>
    /// Get level for monster by current map and stage information
    /// </summary>
    /// <returns></returns>
    [Button]
    public int GetLevelForMonster()
    {
        var currentLevelLocal = currentMapGameData.generalLevelMonster;
        bool isFirstStage = currentMapIndex == 0;
        bool isLastMap = currentStageIndex == currentMapGameData.maxStage;

        if (isFirstStage == false && isLastMap == false)
        {
            var levelPerStage = stageSupport.CalculatorLevelPerStage(currentMapGameData, MapGameConfigs[currentMapIndex + 1]);
            currentLevelLocal += levelPerStage * currentStageIndex;
        }
        return currentLevelLocal;
    }

    public int GetMaxStage()
    {
        return currentMapGameData.maxStage;
    }
    public ItemsStruct GetRandomItemDrop()
    {
        var randomIndex = Random.Range(0, currentMapGameData.itemDrop.Count);
        return currentMapGameData.itemDrop[randomIndex];
    }
    public void ResetStage()
    {
        // có thể là sau khi thua ở stage cuối sẽ không reset về cuối map mà chỉ hồi sinh lại và tiếp túc đánh
        currentStageIndex--;
        if(currentMapIndex < 0 )
            currentMapIndex = 0;
        //pointCollected = 0;
    }
    public void IncreasePointWhenKillMonster() => pointCollected += pointsPerMonsterKill;

    public void IncreasePoint(int point)
    {
        pointCollected += point;
        if (pointCollected > GetPointNeedOfStage())
        {
            pointCollected = GetPointNeedOfStage();
            // Có thể cho phép đi tiếp stage thành 1 lựa chọn trên UI
            OnGoNextMapOrGoNextStage();
        }

    }
    public void OnGoNextMapOrGoNextStage()
    {
        // logic qua map hoặc qua stage
        if (++currentStageIndex < currentMapGameData.maxStage)
        {
            if (allowGoNextStage == false) return;
            OnCompleteStage?.Invoke();
        }
        else
        {
            if (allowGoNextMap == false) return;
            OnCompleteMap?.Invoke();
        }
    }

    private int GetPointNeedOfStage(int stageIndex = -1)
    {
        return currentMapGameData.GetPointNeedOfStage(stageIndex == -1 ? currentStageIndex : stageIndex);
    }

}
