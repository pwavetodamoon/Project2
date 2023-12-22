using Sirenix.OdinInspector;
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

    //[SerializeField] List<SpriteBackground> ScrollingBackgroundArray = new List<SpriteBackground>();
    [InfoBox("KEY OF DICTIONARY JUST TEMPORARY RIGHT NOW !!!!\r\nWE NEED USE ENUM LIKE A KEY WHEN MAKING CORE FEARTURE OF MAP !!!")]
    private static string messenge = "KEY OF DICTIONARY JUST TEMPORARY RIGHT NOW !!!!\r\nWE NEED USE ENUM LIKE A KEY WHEN MAKING CORE FEARTURE OF MAP !!!";

    [SerializeField] private Dictionary<string, SpriteBackground> keyValuePairs = new Dictionary<string, SpriteBackground>();
    private string[] mapsName = { "Map 1", "Map 2", "Map 3", "Map 4", "Map 5", "Map 6", "Map 7", "Map 8", "Map 9" };
    [SerializeField] private string plane;
    [SerializeField] private string frontWall;
    [SerializeField] private string backWall;
    [SerializeField] private string groundDecor;

    [SerializeField] private Texture2D[] ListOfFiles;

    [Button]
    private void Clear()
    {
        keyValuePairs.Clear();
    }

    [Button]
    private void LoadAllSpriteForBackground()
    {
        for (int i = 0; i < mapsName.Length; i++)
        {
            LoadSpriteBackgroundList(mapsName[i]);
        }
        // Debug log warning with rich text format
        Debug.LogWarning("<color=red>" + messenge + "</color>");

        //Debug.LogWarning(messenge);
    }

    [InfoBox("This button just use for testing, not use in outside")]
    [Button]
    private void LoadSpriteBackgroundList(string mapKey)
    {
        // Load String Path
        StringBuilder BackgroundFilePath = new StringBuilder("Background/");
        BackgroundFilePath.Append(mapKey);
        // Load All Texture2D of Map folder
        ListOfFiles = Resources.LoadAll<Texture2D>(BackgroundFilePath.ToString());
        if (ListOfFiles == null || ListOfFiles.Length == 0)
            return;
        // create new SpriteBackground
        var newSpriteBackground = InitSpriteBackground();

        // Add to dictionary
        keyValuePairs.Add(mapKey, newSpriteBackground);
    }

    private SpriteBackground InitSpriteBackground()
    {
        var newSpriteBackground = new SpriteBackground();
        foreach (Texture2D background in ListOfFiles)
        {
            if (background.name == plane)
            {
                newSpriteBackground.plane = background;
            }
            else if (background.name == frontWall)
            {
                newSpriteBackground.frontWall = background;
            }
            else if (background.name == backWall)
            {
                newSpriteBackground.backWall = background;
            }
            else if (background.name == groundDecor)
            {
                newSpriteBackground.groundDecor = background;
            }
        }
        return newSpriteBackground;
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