using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "MapBackgroundSO", menuName = "ScriptableObjects/MapBackgroundSO", order = 1)]
public class MapBackgroundSO : SerializedScriptableObject
{
    // TODO:'KEY' OF DICTIONARY JUST TEMPORARY RIGHT NOW


    [InfoBox("KEY OF DICTIONARY JUST TEMPORARY RIGHT NOW !!!!\r\nWE NEED USE ENUM LIKE A KEY WHEN MAKING CORE FEARTURE OF MAP !!!")]
    private static string messenge = "KEY OF DICTIONARY JUST TEMPORARY RIGHT NOW !!!!\r\nWE NEED USE ENUM LIKE A KEY WHEN MAKING CORE FEARTURE OF MAP !!!";

    [SerializeField] private Dictionary<string, SpritesBackground> keyValuePairs = new Dictionary<string, SpritesBackground>();
    private string[] mapsName = { "Map 1", "Map 2", "Map 3", "Map 4", "Map 5", "Map 6", "Map 7", "Map 8", "Map 9" };
    [SerializeField] private string plane; // name: battleground
    [SerializeField] private string frontWall; // back_decor
    [SerializeField] private string backWall; // back_land
    [SerializeField] private string groundDecor; // ground_decor

    [SerializeField] private Texture2D[] CurrentListOfTexture;

    [Button]
    private void Clear()
    {
        keyValuePairs.Clear();
    }

    /// <summary>
    /// Loads all the sprites for the background.
    /// </summary>
    [Button]
    private void LoadAllTextureForMap()
    {
        for (int i = 0; i < mapsName.Length; i++)
        {
            keyValuePairs.Add(mapsName[i], LoadCurrentTextureOfBackground(mapsName[i]));
        }
        // Debug log warning with rich text format
        Debug.LogWarning("<color=red>" + messenge + "</color>");

        //Debug.LogWarning(messenge);
    }

    [InfoBox("This button just use for testing, not use in outside")]
    [Button]
    private SpritesBackground LoadCurrentTextureOfBackground(string mapKey)
    {
        // Load String Path
        StringBuilder filePath = new StringBuilder("Background/");
        filePath.Append(mapKey);
        // Load All Texture2D of Map folder, Ex: Resources/Background/Map 1
        CurrentListOfTexture = Resources.LoadAll<Texture2D>(filePath.ToString());
        if (CurrentListOfTexture == null || CurrentListOfTexture.Length == 0)
            return new SpritesBackground();
        // init new SpriteBackground
        return InitSpriteBackground();
    }

    private SpritesBackground InitSpriteBackground()
    {
        var newSpriteBackground = new SpritesBackground();
        foreach (Texture2D background in CurrentListOfTexture)
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

    public SpritesBackground GetSpritesBackground(int mapIndex)
    {
        if (keyValuePairs.Count == 0)
        {
            LoadAllTextureForMap();
        }
        if (mapIndex < 0 || mapIndex >= mapsName.Length)
        {
            mapIndex = 0;
        }
        var mapName = mapsName[mapIndex];
        return keyValuePairs[mapName];
    }
}
// Structer of background
[System.Serializable]
public struct SpritesBackground
{
    public Texture2D plane;
    public Texture2D frontWall;
    public Texture2D backWall;
    public Texture2D groundDecor;
}