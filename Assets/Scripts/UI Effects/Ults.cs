using UnityEngine;
using DG.Tweening;
using System;
using Sequence = DG.Tweening.Sequence;

namespace HHP.Ults.UIAnim
{
    public static class UIAnim
    {
        public static void ZoomInScale<T>(T[] obj) where T : Transform
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
        public static void  ZoomInOutScale<T>(T obj, Action onCompleteAction = null)where T : Transform
        {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(obj.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).SetEase(Ease.Linear));
                sequence.Append(obj.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.Linear)).OnComplete(() =>
                {
                    onCompleteAction?.Invoke();
                    sequence.Kill();
                });
        }
        public static void  ZoomOutScale<T>(T obj , Action onCompleteAction = null) where T : Transform
        {
           
            obj.transform.DOScale(new Vector3(0f,0f,0f), 0.2f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                onCompleteAction?.Invoke();
                obj.DOKill();
            }); 
        }

        public static void MoveUIUp(RectTransform gameObjRectTransform)
        {
            Vector3 currentPos = gameObjRectTransform.transform.position;
            gameObjRectTransform.transform.position = new Vector3(0.0f, -gameObjRectTransform.rect.height, 0.0f);
            gameObjRectTransform.DOMove( currentPos, 0.5f);
        }
        public static void MoveDownUI(RectTransform gameObjRectTransform)
        {
            Vector3 currentPos = gameObjRectTransform.transform.position;
            gameObjRectTransform.transform.position = new Vector3(0.0f, gameObjRectTransform.rect.height, 0.0f);
            gameObjRectTransform.DOMove( currentPos, 0.5f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    gameObjRectTransform.DOKill();
                }); 
        }
        public static void MoveUIFormLeft(RectTransform gameObjRectTransform)
        {
            Vector3 currentPos = gameObjRectTransform.transform.position;
            gameObjRectTransform.transform.position = new Vector3(-gameObjRectTransform.rect.width, 0.0f, 0.0f);
            gameObjRectTransform.DOMove( currentPos, 0.5f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    gameObjRectTransform.DOKill();
                });
        }   public static void MoveUIFormRight(RectTransform gameObjRectTransform)
        {
            Vector3 currentPos = gameObjRectTransform.transform.position;
            gameObjRectTransform.transform.position = new Vector3(gameObjRectTransform.rect.width, 0.0f, 0.0f);
            gameObjRectTransform.DOMove( currentPos, 0.5f).SetEase(Ease.Linear)
                .OnComplete(() =>
            {
                gameObjRectTransform.DOKill();
            });
        }

    }

  
}

