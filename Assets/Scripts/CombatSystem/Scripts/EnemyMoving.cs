using UnityEngine;

namespace CombatSystem.Scripts
{
    public class EnemyMoving : MonoBehaviour
    {
        public float Speed = 2;
        private bool notMoving;

        public void SetNotMoving(bool boolen)
        {
            notMoving = boolen;
        }

        public void Moving(Vector2 moveVector, float speed = 2)
        {
            if (notMoving) return;
            Speed = speed;
            transform.Translate(moveVector * (Time.deltaTime * speed));
        }
    }
}