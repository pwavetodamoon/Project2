using System;
using UnityEngine;
using UnityEngine.Rendering;

//[ExecuteAlways]
namespace SortingLayers
{
    [RequireComponent(typeof(SortingGroup))]
    public class SortingLayerByYAxis : MonoBehaviour
    {
        public float yAxis;
        [SerializeField] private SortingGroup sortingGroup;
        [SerializeField] private float lastYPosition;
        [SerializeField] private bool isValidate;

        private void Awake()
        {
            sortingGroup = GetComponent<SortingGroup>();
            isValidate = sortingGroup != null;
        }
        public void PauseSortingLayer()
        {
            isValidate = false;
        }
        public void ResumeSortingLayer()
        {
            isValidate = true;
        }
        public void SetOrderToHighest()
        {
            sortingGroup.sortingOrder = 1000;
        }
        private void Update()
        {
            if (isValidate == false)
                return;
            if (Math.Abs(transform.position.y - lastYPosition) > 0.01f)
            {
                CalculatorOrderLayer();
                lastYPosition = transform.position.y;
            }
        }

        private void CalculatorOrderLayer()
        {
            yAxis = transform.position.y;
            var orderLayer = Mathf.RoundToInt(yAxis * 100);
            orderLayer = Mathf.Clamp(orderLayer, int.MinValue, int.MaxValue);
            sortingGroup.sortingOrder = -orderLayer;
        }
    }
}