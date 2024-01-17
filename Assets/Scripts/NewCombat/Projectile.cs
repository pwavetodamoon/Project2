using NewCombat.Characters;
using UnityEngine;

namespace NewCombat
{
    public class Projectile : MonoBehaviour
    {
        private BoxCollider2D boxCollder2D;
        private new Rigidbody2D rigidbody2D;
        public EntityCharacter shooter;
        private void Awake()
        {
            boxCollder2D = GetComponent<BoxCollider2D>();
            rigidbody2D = boxCollder2D.attachedRigidbody;
            boxCollder2D.isTrigger = true;
        }
        public void SetShooter(EntityCharacter shooter)
        {
            this.shooter = shooter;
        }
        public void SetTarget(Transform target)
        {
            var direction = target.position - transform.position;
            direction.Normalize();
            SetVelocity(direction);
        }
        public void SetVelocity(Vector2 direction, float speed = 5)
        {
            rigidbody2D.velocity = direction * speed;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //boxCollder2D.enabled = false;
            Debug.Log(collision.name);
            var boxes = Physics2D.OverlapBoxAll(transform.position, boxCollder2D.size, 0);
            foreach (var box in boxes)
            {
                if (box.TryGetComponent(out MonsterCharacter monsterCharacter))
                {
                    Debug.Log(shooter + " is hit: " + monsterCharacter.name);
                    box.GetComponent<IDamageable>().TakeDamage(shooter.BaseStats.Strength);
                }
            }
            transform.gameObject.SetActive(false);
        }
    }
}
