using ObjectPool;
using System;
using UnityEngine;

namespace NewCombat.Projectiles
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ProjectileBase : MonoBehaviour, IPooled<ProjectileBase>
    {
        [SerializeField] private Collider2D _Collder2D;
        [SerializeField] protected Transform target;
        [SerializeField] protected string Tag;
        protected bool isAttack;
        private float TimeOut = 10f;
        protected Action OnEndAttack;

        public Action<ProjectileBase> ReleaseCallback { get ; set ; }

        public void RegisterOnEndVfx(Action method)
        {
            OnEndAttack += method;
        }

        private void OnDisable()
        {
            OnEndAttack = null;
        }

        protected virtual void Awake()
        {
            _Collder2D = GetComponent<Collider2D>();
            _Collder2D.isTrigger = true;
        }

        public virtual void Initialized(Transform target, string tag)
        {
            this.target = target;
            Tag = tag;
            isAttack = false;
        }

        public virtual void Release()
        {
            OnEndAttack?.Invoke();
            ReleaseCallback?.Invoke(this);
        }
    }
}