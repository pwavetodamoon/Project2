using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject target;
    public float duration = 1.0f;
    public Ease ease = Ease.Linear;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [Button]
    void Moving()
    {
        var position = target.transform.position;
        var originalPostion = transform.position;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(position, duration * 2).SetEase(Ease.OutCubic));
        sequence.Append(transform.DOMove(originalPostion, duration * 2).SetEase(Ease.OutCubic));
        //transform.DOMove(position, duration).SetEase(curve).OnComplete(() =>
        //{
        //    transform.position = originalPostion;
        //});
    }
}
