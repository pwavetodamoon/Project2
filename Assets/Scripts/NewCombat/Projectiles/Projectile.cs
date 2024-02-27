using ObjectPool;
using System;
using UnityEngine;

namespace NewCombat.Projectiles
{
    public class Projectile : ProjectileBase
    {
        [SerializeField] private BoxCollider2D BoxCollder2D;
        private readonly float speed = 20;

        private void Update()
        {
            if (target == null)
            {
                Release();
            }

            if (isOnTarget)
            {
                Debug.Log("Projectile is attacking");
                Release();
            }
            else
            {
                MoveToTarget();
            }
        }

        private bool isOnTarget => Vector3.Distance(transform.position, target.transform.position) < 0.1f;

        private void MoveToTarget()
        {
            var targetPosition = target.transform.position;
            var direction = targetPosition - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.right = direction;
        }
        public override void Release()
        {
            OnAttack?.Invoke();
            ReleaseCallback?.Invoke(this);
        }
    }
}