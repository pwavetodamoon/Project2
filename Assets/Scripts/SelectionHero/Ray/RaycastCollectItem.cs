using Item;
using UnityEngine;

namespace SelectionHero.Ray
{
    public class RaycastCollectItem : MonoBehaviour
    {
        public LayerMask layerMask;
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                var baseDrop = hit.collider.GetComponent<BaseDrop>();
                if (baseDrop != null)
                {
                    Debug.Log("check");
                    baseDrop.Collect();
                }
            }
        }
    }
}