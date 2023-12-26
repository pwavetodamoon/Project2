using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Represents the map background in the game.
/// </summary>
public class MapBackground : MonoBehaviour
{
    [SerializeField] private MapBackgroundSO mapBackgroundSO;

    //[SerializeField] private ScrollingBackground Plane;
    //[SerializeField] private ScrollingBackground FrontWall;
    //[SerializeField] private ScrollingBackground BackWall;
    //[SerializeField] private ScrollingBackground GroundDecor;
    [InfoBox("Map name use for testing, just on play mode, it just temporary")]
    public int MapIndex = 0;

    [ShowInInspector] private ScrollingBackground[] ScrollingBackgroundArray;

    private void Start()
    {
        LoadField();
        LoadTexture();
        StartScrolling();
    }

    /// <summary>
    /// Loads the field by getting the components of ScrollingBackground in the children objects.
    /// </summary>
    [Button]
    private void LoadField()
    {
        ScrollingBackgroundArray = GetComponentsInChildren<ScrollingBackground>();

        //Plane = transform.Find("Plane").GetComponent<ScrollingBackground>();
        //FrontWall = transform.Find("FrontWall").GetComponent<ScrollingBackground>();
        //BackWall = transform.Find("BackWall").GetComponent<ScrollingBackground>();
        //GroundDecor = transform.Find("GroundDecor").GetComponent<ScrollingBackground>();
    }

    [InfoBox("Load all texure for background array, use in play mode")]
    /// <summary>
    /// Loads the textures for the map background.
    /// </summary>
    [Button]
    [DisableInEditorMode]
    public void LoadTexture()
    {
        if (ScrollingBackgroundArray == null || ScrollingBackgroundArray.Length == 0)
        {
            LoadField();
        }

        SpritesBackground spriteBackground = mapBackgroundSO.GetSpritesBackground(MapIndex);
        var texture2dPlane = spriteBackground.plane;
        var texture2dBackWall = spriteBackground.backWall;
        var texture2dFrontWall = spriteBackground.frontWall;
        var texture2dGroundDecor = spriteBackground.groundDecor;

        ScrollingBackgroundArray[0].UpdateCurrentTexture(texture2dPlane);
        ScrollingBackgroundArray[0].UpdateSortingOrder(0);

        ScrollingBackgroundArray[1].UpdateCurrentTexture(texture2dBackWall);
        ScrollingBackgroundArray[1].UpdateSortingOrder(1);

        ScrollingBackgroundArray[2].UpdateCurrentTexture(texture2dFrontWall);
        ScrollingBackgroundArray[2].UpdateSortingOrder(2);

        ScrollingBackgroundArray[3].UpdateCurrentTexture(texture2dGroundDecor);
        ScrollingBackgroundArray[3].UpdateSortingOrder(3);

        //Plane.UpdateCurrentTexture(spriteBackground.plane);
        //FrontWall.UpdateCurrentTexture(spriteBackground.frontWall);
        //BackWall.UpdateCurrentTexture(spriteBackground.backWall);
        //GroundDecor.UpdateCurrentTexture(spriteBackground.groundDecor);
    }

    /// <summary>
    /// Adjusts the scrolling speed of the background.
    /// </summary>
    /// <param name="speed">The speed value to adjust the scrolling speed.</param>
    [Button]
    public void AdjustSpeed(float speed = .5f)
    {
        foreach (var item in ScrollingBackgroundArray)
        {
            item.AdjustSpeed(speed);
        }
    }

    /// <summary>
    /// Starts scrolling the background.
    /// </summary>
    [Button]
    public void StartScrolling()
    {
        foreach (var item in ScrollingBackgroundArray)
        {
            item.Resume();
        }
    }

    /// <summary>
    /// Stops scrolling the background.
    /// </summary>
    [Button]
    public void StopScrolling()
    {
        foreach (var item in ScrollingBackgroundArray)
        {
            item.Pause();
        }
    }
}