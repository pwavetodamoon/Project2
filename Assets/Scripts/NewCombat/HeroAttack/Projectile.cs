using NewCombat.Characters;
using UnityEngine;

namespace NewCombat.HeroAttack
{
    public class Projectile : MonoBehaviour
    {
        private BoxCollider2D boxCollder2D;
        private new Rigidbody2D rigidbody2D;
        private CombatCollider CombatCollider;
        public string Tag;
        private void Awake()
        {
            boxCollder2D = GetComponent<BoxCollider2D>();
            rigidbody2D = boxCollder2D.attachedRigidbody;
            boxCollder2D.isTrigger = true;
        }
        public void Initialized(EntityCharacter shooter, Transform target,string tag)
        {
            Tag = tag;
            CombatCollider = new CombatCollider(shooter.GetComponent<BaseStats>());
            SetTargetDirection(target);
        }
        private void SetTargetDirection(Transform target)
        {
            var direction = target.position - transform.position;
            direction.Normalize();
            SetVelocity(direction);
        }
        private void SetVelocity(Vector2 direction, float speed = 5)
        {
            rigidbody2D.velocity = direction * speed;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //boxCollder2D.enabled = false;
            Debug.Log(collision.name);
            CombatCollider.CheckOverlapBox(Tag, transform.position, boxCollder2D.size, 0);

            transform.gameObject.SetActive(false);
        }
    }
}
