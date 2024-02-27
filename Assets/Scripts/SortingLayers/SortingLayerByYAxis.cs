using System;
using UnityEngine;
using UnityEngine.Rendering;

//[ExecuteAlways]
namespace SortingLayers
{
    [RequireComponent(typeof(SortingGroup))]
    public class SortingLayerByYAxis : MonoBehaviour
    {
        public float yAxis = 0;
        [SerializeField] private SortingGroup sortingGroup;
        [SerializeField] private float lastYPosition;
        [SerializeField] bool isValidate = false;

        private void Awake()
        {
            sortingGroup = GetComponent<SortingGroup>();
            isValidate = sortingGroup != null;
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
