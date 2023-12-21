using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBackground : MonoBehaviour
{
    public float speed = 0.5f;
    public bool isPause = false;
    [SerializeField] private float offset;

    [SerializeField] List<Material> materialList = new List<Material>();
    public MapBackgroundSO mapBackgroundSO;
    // Start is called before the first frame update
    void Start()
    {

        LoadAllMaterial();
        StartCoroutine(ScrollHorizontalAll());
    }
    private void LoadAllMaterial()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        int sortingOrder = 0;
        foreach (Renderer item in renderers)
        {
            materialList.Add(item.material);
            item.sortingOrder = sortingOrder;
            sortingOrder++;
        }
    }

    IEnumerator ScrollHorizontalAll()
    {
        if (materialList.Count == 0) yield return null;
        while (true)
        {
            if (isPause)
                yield return new WaitForEndOfFrame();
            foreach (var item in materialList)
            {
                ScrollHorizontal(item);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }

    }
    private void ScrollHorizontal(Material material)
    {
        if(offset >= float.MaxValue)
        {
            offset = 0;
        }
        offset += (Time.deltaTime * speed) / 10f;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
