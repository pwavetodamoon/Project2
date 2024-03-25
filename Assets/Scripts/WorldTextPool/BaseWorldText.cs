using System;
using System.Collections;
using DG.Tweening;
using ObjectPool;
using Sirenix.OdinInspector;
using SortingLayers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldTextPool
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
            // transform.localScale = Vector3.one;
            StartCoroutine(sortingLayerByTime.DecreaseSortingLayer());
            StartCoroutine(SetTweenDefault());
        }

        protected virtual IEnumerator SetTweenDefault()
        {
            Transform textMesh = textMeshPro.transform;
            float x = .5f;
            var boolen = Random.Range(0, 2) == 0;
            var direction = boolen == true ? new Vector3(x, 1, 0) : new Vector3(-x, 1, 0);
            textMesh.DOMove(textMesh.localPosition + direction, duration * 2);
            textMesh.transform.rotation = Quaternion.Euler(0, 0, boolen == false ? 10 : -10);
            textMesh.transform.DOScale(Vector2.one, duration).OnComplete(() =>
            {
                textMeshPro.DOColor(Color.clear, duration);
                sortingLayerByTime.ChangeGoDownState(true);
            });
            yield return new WaitForSeconds(duration * 2 + duration);
            Release();

        }

        [Button]
        public void SetColor(Color color)
        {
            textMeshPro.color = color;
        }
    }
}