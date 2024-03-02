using DropItem;
using UnityEngine;

namespace RaycastManager
{
    public class RaycastCollectItem : MonoBehaviour
    {
        Camera mainCamera;
        public LayerMask layerMask;
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                Items item = hit.collider.GetComponent<Items>();
                if (item != null)
                {
                    Debug.Log("check");
                    item.Collect();
                }
            }
        }
    }
}

