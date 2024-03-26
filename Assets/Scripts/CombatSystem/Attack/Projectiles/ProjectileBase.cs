using System;
using ObjectPool;
using UnityEngine;

namespace CombatSystem.Attack.Projectiles
{
    public abstract class ProjectileBase : MonoBehaviour, IPooled<ProjectileBase>
    {
        [SerializeField] protected Transform target;
        [SerializeField] protected string Tag;
        protected bool isAttack;
        protected Action OnEndAttack;
        // private float TimeOut = 10f;
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