using System;
using CombatSystem.Attack.Projectiles;
using CombatSystem.Entity;
using Helper;
using ObjectPool;
using UnityEngine;

namespace CombatSystem.Attack.Utilities
{
    public enum RangedProjectile
    {
        Arrow,
        Magic1,
        FireBall
    }
    public enum MagicProjectileType
    {
        Magic1,
        Magic2,
        Magic3
    }

    public class PrefabAttackFactoryPool : Singleton<PrefabAttackFactoryPool>
    {
        // Use in Scene Environment
        public Projectile ProjectilePrefab;
        public MagicProjectile MagicProjectilePrefab;
        public ObjectPoolPrefab<ProjectileBase> magicProjectile_pool;
        public ObjectPoolPrefab<ProjectileBase> projectile_pool;

        protected override void Awake()
        {
            base.Awake();
            var go = new GameObject();
            go.transform.position = Vector2.zero;
            go.name = "PoolHolder";
            projectile_pool = new ObjectPoolPrefab<ProjectileBase>(ProjectilePrefab, go.transform, 5);
            magicProjectile_pool = new ObjectPoolPrefab<ProjectileBase>(MagicProjectilePrefab, go.transform, 5);
        }

        public ProjectileBase Get(RangedProjectile type)
        {
            switch (type)
            {
                case RangedProjectile.Arrow:
                    return projectile_pool.Get();
            }

            return null;
        }
        public ProjectileBase Get(MagicProjectileType type)
        {
            switch (type)
            {
                case MagicProjectileType.Magic1:
                    return magicProjectile_pool.Get();
            }

            return null;
        }
        public ProjectileBase SpawnProjectile(EntityCharacter monster, Transform attackTransform, Action action, string GameTag, RangedProjectile type)
        {
            var projectile = Get(type);
            projectile.transform.position = attackTransform.position;
            projectile.RegisterOnEndVfx(action);
            projectile.Initialized(monster.transform, GameTag);
            return projectile;
        }
    }
}