using UnityEngine;

namespace Item
{
    public class ItemMove : MonoBehaviour
    {
        public float speed = 3f;
        public Vector2 moveVector = Vector2.left;

        public void Update()
        {
            transform.Translate(moveVector * (Time.deltaTime * speed));
        }
    }
}