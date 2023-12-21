using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "MapBackgroundSO", menuName = "ScriptableObjects/MapBackgroundSO", order = 1)]
public class MapBackgroundSO : SerializedScriptableObject
{
    /*
     *  KEY OF DICTIONARY JUST TEMPORARY RIGHT NOW
     *  WE NEED USE ENUM LIKE A KEY WHEN MAKING CORE FEARTURE OF MAP
     */
    //[SerializeField] List<SpriteBackground> spriteBackgrounds = new List<SpriteBackground>();
    [InfoBox("KEY OF DICTIONARY JUST TEMPORARY RIGHT NOW !!!!\r\nWE NEED USE ENUM LIKE A KEY WHEN MAKING CORE FEARTURE OF MAP !!!")]

    static string messenge = "KEY OF DICTIONARY JUST TEMPORARY RIGHT NOW !!!!\r\nWE NEED USE ENUM LIKE A KEY WHEN MAKING CORE FEARTURE OF MAP !!!";


    [SerializeField] Dictionary<string,SpriteBackground> keyValuePairs = new Dictionary<string,SpriteBackground>();
    string[] mapsName = { "Map 1", "Map 2", "Map 3", "Map 4", "Map 5", "Map 6", "Map 7", "Map 8", "Map 9" };
    [SerializeField] string plane;
    [SerializeField] string frontWall;
    [SerializeField] string backWall;
    [SerializeField] string groundDecor;

    [SerializeField] Texture2D[] ListOfFiles;
    [Button]
    void Clear()
    {
        keyValuePairs.Clear();
    }
    [Button]
    void LoadAllSpriteForBackground()
    {
        
        for(int i = 0; i < mapsName.Length; i++)
        {
            LoadSpriteBackgroundList(mapsName[i]);
        }
        // Debug log warning with rich text format
        Debug.LogWarning("<color=red>" + messenge + "</color>");

        //Debug.LogWarning(messenge);
    }
    [Button]
    void LoadSpriteBackgroundList(string mapName)
    {
        StringBuilder sb = new StringBuilder("Background/");
        sb.Append(mapName);
        ListOfFiles = Resources.LoadAll<Texture2D>(sb.ToString());
        if (ListOfFiles == null || ListOfFiles.Length == 0)
            return;
        var newSpriteBackground = new SpriteBackground();
        foreach(Texture2D background in ListOfFiles)
        {
            if(background.name ==  plane)
            {
                newSpriteBackground.plane = background;
            }
            else if(background.name == frontWall)
            {
                newSpriteBackground.frontWall = background;
            }
            else if(background.name == backWall)
            {
                newSpriteBackground.backWall = background;
            }
            else if(background.name == groundDecor)
            {
                newSpriteBackground.groundDecor = background;
            }
        }
        keyValuePairs.Add(mapName,newSpriteBackground);
    }
    public SpriteBackground GetSpriteBackground(string mapName)
    {
        return keyValuePairs[mapName];
    }
    

}
[System.Serializable]
public struct SpriteBackground
{
    public Texture2D plane;
    public Texture2D frontWall;
    public Texture2D backWall;
    public Texture2D groundDecor;
}
