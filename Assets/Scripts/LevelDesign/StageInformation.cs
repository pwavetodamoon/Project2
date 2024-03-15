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
    // TODO: Custom monster level per stage

    [Button]
    public int GetLevelForMonster()
    {
        var currentLevel = currentMapGameData.generalLevelMonster;
        if (currentMapIndex + 1 < MapGameConfigs.Length && currentStageIndex > 0)
        {
            var nextMapData = MapGameConfigs[currentMapIndex + 1];
            var levelPerStage = stageSupport.CalculatorLevelPerStage(currentMapGameData, nextMapData);
            currentLevel += levelPerStage * currentStageIndex;
        }
        // Debug.Log("Current Level: " + currentLevel);
        return currentLevel;
    }


    public ItemsStruct GetRandomItemDrop()
    {
        var randomIndex = Random.Range(0, currentMapGameData.itemDrop.Count);
        return currentMapGameData.itemDrop[randomIndex];
    }
    public void ResetStage()
    {
        currentStageIndex = 0;
        pointCollected = 0;
    }
    public void IncreasePointWhenKillMonster()
    {
        pointCollected += pointsPerMonsterKill;
    }
    public void IncreasePoint(int point)
    {
        pointCollected += point;
        if (pointCollected > GetPointNeedOfStage())
        {
            pointCollected = GetPointNeedOfStage();
            OnGoNextMapOrGoNextStage();
        }

    }
    private void OnGoNextMapOrGoNextStage()
    {
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
