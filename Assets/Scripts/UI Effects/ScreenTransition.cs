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
    public YieldInstruction waitBetweenTransition = new WaitForSeconds(.5f);

    public IEnumerator StartTransition()
    {
        yield return image.DOFillAmount(1, duration).SetEase(Ease.OutQuart).WaitForCompletion();
    }

    public IEnumerator EndTransition()
    {
        yield return image.DOFillAmount(0, duration).SetEase(Ease.OutQuart).WaitForCompletion();
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Transition();
    //    }
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        image.fillAmount = 0;
    //    }
    //}
}
