using Core.Items.InScreen;
using UnityEngine;

namespace Core.Items
{
    public class RayCollect : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, mousePos);
            Debug.DrawRay(transform.position, mousePos, Color.red);
            if (hit2D.collider != null)
            {
                if (hit2D.collider.TryGetComponent(out ICollect icollect))
                {
                    icollect.Gather();
                }
            }
        }
    }
}
