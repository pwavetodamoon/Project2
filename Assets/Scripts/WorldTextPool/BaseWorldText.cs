using System;
using DG.Tweening;
using ObjectPool;
using Sirenix.OdinInspector;
using SortingLayers;
using TMPro;
using UnityEngine;

namespace WorldText
{
    [RequireComponent(typeof(SortingLayerByTime))]
    public class BaseWorldText : MonoBehaviour, IPooled<BaseWorldText>
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

        private void Awake()
        {
            textMeshPro = GetComponent<TextMeshPro>();
            sortingLayerByTime = GetComponent<SortingLayerByTime>();
        }

        public void Release()
        {
            StopAllCoroutines();
            transform.DOKill();
            ReleaseCallback?.Invoke(this);
        }

        public Action<BaseWorldText> ReleaseCallback { get; set; }

        private void SetText(string str)
        {
            textMeshPro.text = str;
        }

        public void Init()
        {
            transform.localScale = Vector3.one;
            StartCoroutine(sortingLayerByTime.DecreaseSortingLayer());
            SetTweenDefault(transform);
        }

        protected virtual void SetTweenDefault(Transform textMesh)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(textMesh.transform.DOLocalMove(
                textMesh.transform.localPosition + Vector3.up, duration).OnComplete(() =>
            {
                textMeshPro.DOFade(0f, duration / 2);
            }));
            sequence.Append(textMesh.transform.DOScale(Vector3.zero, duration / 2));
            //sequence.Join(textMeshPro.DOFade(.5f, 1));
            sequence.OnComplete(() => { Release(); });
        }

        [Button]
        public void SetColor(Color color)
        {
            textMeshPro.color = color;
        }
    }
}