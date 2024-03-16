using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace Ults
{
    public static class UIAnim
    {
        public static void InScale<T>(T[] obj) where T : Transform
        {
            foreach (Transform item in obj)
            {
                item.localScale = Vector3.zero;

                item.DOScale(Vector3.one, 0.5f)
                    .SetEase(Ease.OutBack)
                   .OnComplete(() => { item.DOKill(); });
                ;
            }

        }
        public static void  ScaleInOut(Button obj, Action onCompleteAction = null)
        {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(obj.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).SetEase(Ease.Linear));
                sequence.Append(obj.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.Linear)).OnComplete(() =>
                {
                    onCompleteAction?.Invoke();
                    sequence.Kill();
                });
        }
        public static void  OutScale<T>(T obj , Action onCompleteAction = null) where T : Transform
        {
           
            obj.transform.DOScale(new Vector3(0f,0f,0f), 0.2f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                onCompleteAction?.Invoke();
                obj.DOKill();
            }); 
        }

    }

  
}

