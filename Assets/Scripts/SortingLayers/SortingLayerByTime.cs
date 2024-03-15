using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace SortingLayers
{
    [RequireComponent(typeof(SortingGroup))]
    public class SortingLayerByTime : MonoBehaviour
    {
        [SerializeField] private SortingGroup sortingGroup;
        [SerializeField][Min(0f)] private float sortingDuration = .1f;
        [SerializeField] private int sortingOrderDefault = 100;
        private bool goDown = false;
        private void Awake()
        {
            sortingGroup = GetComponent<SortingGroup>();
        }
        public void ChangeGoDownState(bool _goDown)
        {
            goDown = _goDown;
        }
        private void OnEnable()
        {
            sortingGroup.sortingOrder = 0;
            goDown = false;
        }

        public IEnumerator DecreaseSortingLayer()
        {
            while (true)
            {
                if (goDown)
                {
                    sortingGroup.sortingOrder--;
                }
                else
                {
                    sortingGroup.sortingOrder++;
                }
                yield return new WaitForSeconds(sortingDuration);
            }
        }
    }
}