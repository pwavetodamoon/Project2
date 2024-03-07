using System.Collections;
using Helper;
using UnityEngine;

namespace NewCombat.Projectiles
{
    [RequireComponent(typeof(GetAnimationLength))]
    public class MagicProjectile : ProjectileBase
    {
        private float animationTime;
        private Animator animator;
        protected CircleCollider2D CircleCollider2D;
        private WaitForSeconds waitForEndAnim;

        protected override void Awake()
        {
            base.Awake();
            animationTime = GetComponent<GetAnimationLength>().length;
            waitForEndAnim = new WaitForSeconds(animationTime);
            CircleCollider2D = GetComponent<CircleCollider2D>();
            animator = GetComponent<Animator>();
        }

        public override void Initialized(Transform target, string tag)
        {
            base.Initialized(target, tag);
            transform.position = target.position;
            animator.enabled = false;
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            animator.enabled = true;
            yield return waitForEndAnim;
            Release();
        }
    }
}