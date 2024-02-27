using System;
using DG.Tweening;
using ObjectPool;
using SortingLayers;
using TMPro;
using UnityEngine;

namespace WorldTextPool
{
    [RequireComponent(typeof(SortingLayerByTime))]
    public class BaseWorldText : MonoBehaviour , IPooled<BaseWorldText>
    {
        [SerializeField] private float duration = .5f;
        [SerializeField] private int fontSize = 5;
        [SerializeField] private TextMeshPro textMeshPro;
        private SortingLayerByTime sortingLayerByTime;
        public string Text
        {
            get => textMeshPro.text;
            set => SetText(value);
        }

        private void SetText(string str)
        {
            textMeshPro.text = str;
        }
        private void Awake()
        {
            textMeshPro = GetComponent<TextMeshPro>();
            sortingLayerByTime = GetComponent<SortingLayerByTime>();
        }

        public void Init()
        {
            StartCoroutine(sortingLayerByTime.DecreaseSortingLayer());
            SetTweenDefault(transform);
        }

        protected virtual void SetTweenDefault(Transform textMesh)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(textMesh.transform.DOLocalMove(
                textMesh.transform.localPosition + Vector3.up, duration));
            sequence.OnComplete(() =>
            {
                Release();
            });
        }

        public void Release()
        {
            StopAllCoroutines();
            transform.DOKill();
            ReleaseCallback?.Invoke(this);
        }

        public Action<BaseWorldText> ReleaseCallback {get; set; }
    }
}
