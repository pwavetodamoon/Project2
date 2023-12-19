using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 0.5f;

    [SerializeField] private float offset;
    private Material material;
    [SerializeField] private bool isPaused = false;
    [SerializeField] private int sortingOrder = 0;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        var tr = GetComponent<Renderer>();
        //tr.sortingLayerName = "SubBackground";
        tr.sortingOrder = sortingOrder;
        //Debug.Log(tr.sortingLayerName);
    }
    [Button]
    public void UpdateCurrentTexture(Texture2D texture)
    {
        if (texture != null)
        {
            material.mainTexture = texture;
        }
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void AdjustSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    private void Update()
    {
        if (isPaused)
        {
            return;
        }
        if (material == null)
        {
            material = GetComponent<Renderer>().material;
        }
        if (offset >= float.MaxValue)
        {
            offset = 0;
        }
        ScrollHorizontal();
    }

    private void ScrollHorizontal()
    {
        offset += (Time.deltaTime * speed) / 10f;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}