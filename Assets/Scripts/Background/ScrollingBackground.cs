using Sirenix.OdinInspector;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    [SerializeField] private float offset;
    private Material material;
    [SerializeField] private bool isPaused = true;
    [SerializeField] private int sortingOrder = 0;
    private Renderer tr;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        //tr.sortingLayerName = "SubBackground";
        //Debug.Log(tr.sortingLayerName);
    }

    [Button]
    public void UpdateSortingOrder(int order)
    {
        tr = GetComponent<Renderer>();
        tr.sortingOrder = sortingOrder;
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
        ScrollHorizontal();
    }

    public void ScrollHorizontal()
    {
        if (isPaused)
        {
            return;
        }
        if (offset >= float.MaxValue)
        {
            offset = 0;
        }
        offset += (Time.deltaTime * speed) / 10f;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}