using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
public class ScreenTransition : MonoBehaviour
{
    public Image image;
    public float duration;
    public float durationInTran = .25f;
    [Button]
    public void Transition()
    {
        StartCoroutine(TransitionCoroutine());
    }

    IEnumerator TransitionCoroutine()
    {
        yield return image.DOFillAmount(1, duration).SetEase(Ease.OutQuart).WaitForCompletion();
        yield return new WaitForSeconds(durationInTran);
        yield return image.DOFillAmount(0, duration).SetEase(Ease.OutQuart).WaitForCompletion();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transition();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            image.fillAmount = 0;
        }
    }
}
