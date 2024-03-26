using CombatSystem.Attack.Utilities;
using UnityEngine;

namespace CombatSystem.Attack.Projectiles
{
    public class Projectile : ProjectileBase
    {
        private readonly float speed = 20;

        private bool isOnTarget => Vector3.Distance(transform.position, target.transform.position) < 0.1f;

        private void Update()
        {
            if (target == null || isOnTarget)
                Release();
            else
                MoveToTarget();
        }

        public override void Release()
        {
            base.Release();
        }
        private void MoveToTarget()
        {
            var targetPosition = target.transform.position;
            var direction = targetPosition - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.right = direction;
        }
    }
}