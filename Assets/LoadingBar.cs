using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{

    public Slider loadingBar;
    public TextMeshProUGUI loadingText;
    public float time;

    private void Awake()
    {
        StartCoroutine(LoadProcess(time));
    }
    public void StartLoading(float totalTime)
    {
        StartCoroutine(LoadProcess(totalTime));
    }

    IEnumerator LoadProcess(float totalTime)
    {
        float progress = 0;

        while (progress < 1)
        {
            progress += Time.deltaTime / totalTime;
            loadingBar.value = progress ;
            loadingText.text = "LOADING "+ Mathf.Round(progress* 100).ToString() + "%";
            yield return null;
        }
    }
}