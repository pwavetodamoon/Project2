using UnityEngine;

namespace Core.Items.InScreen
{
    public class ItemMoving : MonoBehaviour
    {
        public float speed = 1;
        public bool isMoving = true;

        void FixedUpdate()
        {
            if (!isMoving) return;
            transform.Translate(speed * Time.deltaTime * Vector2.left);
        }
    }
}