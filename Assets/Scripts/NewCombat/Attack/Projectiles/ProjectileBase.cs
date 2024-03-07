using System;
using ObjectPool;
using UnityEngine;

namespace NewCombat.Attack.Projectiles
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ProjectileBase : MonoBehaviour, IPooled<ProjectileBase>
    {
        [SerializeField] private Collider2D _Collder2D;
        [SerializeField] protected Transform target;
        [SerializeField] protected string Tag;
        protected bool isAttack;
        protected Action OnEndAttack;
        private float TimeOut = 10f;

        protected virtual void Awake()
        {
            _Collder2D = GetComponent<Collider2D>();
            _Collder2D.isTrigger = true;
        }

        private void OnDisable()
        {
            OnEndAttack = null;
        }

        public Action<ProjectileBase> ReleaseCallback { get; set; }

        public virtual void Release()
        {
            OnEndAttack?.Invoke();
            ReleaseCallback?.Invoke(this);
        }

        public void RegisterOnEndVfx(Action method)
        {
            OnEndAttack += method;
        }

        public virtual void Initialized(Transform target, string tag)
        {
            this.target = target;
            Tag = tag;
            isAttack = false;
        }
    }
}