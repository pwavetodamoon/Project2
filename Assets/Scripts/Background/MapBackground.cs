using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBackground : MonoBehaviour
{
    [SerializeField] private ScrollingBackground plane;
    [SerializeField] private ScrollingBackground subBg;
    [SerializeField] private ScrollingBackground bg;

    public MapBackgroundSO mapBackgroundSO;
    // Start is called before the first frame update
    void Start()
    {
        plane.UpdateCurrentTexture(mapBackgroundSO.plane);
        subBg.UpdateCurrentTexture(mapBackgroundSO.subBg);
        bg.UpdateCurrentTexture(mapBackgroundSO.bg);

    }

}
