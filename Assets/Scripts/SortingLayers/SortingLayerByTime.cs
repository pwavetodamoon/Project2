using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace SortingLayers
{
    [RequireComponent(typeof(SortingGroup))]
    public class SortingLayerByTime : MonoBehaviour
    {
        [SerializeField] private SortingGroup sortingGroup;
        [SerializeField] [Min(0f)] private float sortingDuration = .1f;
        [SerializeField] private int sortingOrderDefault = 100;

        private void Awake()
        {
            sortingGroup = GetComponent<SortingGroup>();
        }

        private void OnEnable()
        {
            sortingGroup.sortingOrder = sortingOrderDefault;
        }

        public IEnumerator DecreaseSortingLayer()
        {
            while (true)
            {
                yield return new WaitForSeconds(sortingDuration);
                sortingGroup.sortingOrder--;
            }
        }
    }
}