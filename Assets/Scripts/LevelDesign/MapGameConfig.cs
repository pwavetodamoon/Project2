using System;
using Core.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapGameConfig", menuName = "ScriptableObjects/MapGameConfig")]
public class MapGameConfig : ScriptableObject
{
    public int maxStage;
    public int maxPointNeedToComplete;
    public int generalLevelMonster;
    public List<ItemsStruct> itemDrop;

    public int GetPointNeedOfStage(int stageIndex)
    {
        return maxPointNeedToComplete / maxStage *(stageIndex + 1);
    }
}


public class StageSupport
{
    public int CalculatorLevelPerStage(MapGameConfig currentMapGameData, MapGameConfig nextMapGameData)
    {
        var maxStageCurrentMap = currentMapGameData.maxStage;
        var generalLevel1 = currentMapGameData.generalLevelMonster;
        var generalLevel2 = nextMapGameData.generalLevelMonster;
        var levelPerIncrement = (generalLevel2 - generalLevel1) / maxStageCurrentMap;
        return levelPerIncrement;

    }
}
