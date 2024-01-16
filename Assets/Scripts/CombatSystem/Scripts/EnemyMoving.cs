using UnityEngine;

namespace CombatSystem.Scripts
{
    public class EnemyMoving : MonoBehaviour
    {
        public float speed;
        public bool isMoving = true;

        public void Setup(float speed)
        {
            this.speed = speed;
        }

        private void Update()
        {
            Moving();
        }

        public void Moving()
        {
            if (!isMoving) return;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}