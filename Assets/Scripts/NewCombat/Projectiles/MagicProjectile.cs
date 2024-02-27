using ObjectPool;
using System;
using System.Collections;
using Helper;
using UnityEngine;

namespace NewCombat.Projectiles
{
    [RequireComponent(typeof(GetAnimationLength))]
    public class MagicProjectile : ProjectileBase
    {
        protected CircleCollider2D CircleCollider2D;
        private float animationTime = 0;
        private Animator animator;

        public override void Initialized(Transform target, string tag)
        {
            base.Initialized(target, tag);
            transform.position = target.position;
            CircleCollider2D = GetComponent<CircleCollider2D>();
            animator = GetComponent<Animator>();
            animator.enabled = false;
            StartCoroutine("AttackCoroutine");
        }

        private IEnumerator AttackCoroutine()
        {
            animator.enabled = true;
            animationTime = GetComponent<GetAnimationLength>().length;
            OnAttack?.Invoke();
            yield return new WaitForSeconds(animationTime / 2);
            Release();
        }

        public override void Release()
        {
            ReleaseCallback?.Invoke(this);
        }

    }
}