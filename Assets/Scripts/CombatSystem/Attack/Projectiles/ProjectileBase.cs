using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using LevelAndStats;
using ObjectPool;
using System;
using UnityEngine;

namespace CombatSystem.Attack.Projectiles
{
    public abstract class ProjectileBase : MonoBehaviour, IPooled<ProjectileBase>
    {
        [SerializeField] protected Transform target;
        [SerializeField] protected string Tag;
        public EntityStats EntityStats;
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
            if (target == null) return;
            var damageable = target.GetComponent<EntityCharacter>().GetRef<IDamageable>();
            damageable?.TakeDamage(EntityStats);
        }

        public void RegisterOnEndVfx(Action method)
        {
            OnEndAttack += method;
        }

        public virtual void Initialized(Transform target, EntityStats entityStats, string tag)
        {
            this.target = target;
            EntityStats = entityStats;
            Tag = tag;
            isAttack = false;
        }
    }
}