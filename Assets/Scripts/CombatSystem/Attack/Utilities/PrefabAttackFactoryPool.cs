using CombatSystem.Attack.Projectiles;
using CombatSystem.Entity;
using Helper;
using LevelAndStats;
using ObjectPool;
using System;
using UnityEngine;

namespace CombatSystem.Attack.Utilities
{
    public enum RangedProjectileType
    {
        Arrow,
        Magic1,
    }

    public class PrefabAttackFactoryPool : Singleton<PrefabAttackFactoryPool>
    {
        // Use in Scene Environment
        public Projectile ProjectilePrefab;

        public Projectile MagicProjectilePrefab;
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

        public ProjectileBase Get(object type)
        {
            switch (type)
            {
                case RangedProjectileType.Arrow:
                    return projectile_pool.Get();

                case RangedProjectileType.Magic1:
                    return magicProjectile_pool.Get();
            }

            return null;
        }

        public ProjectileBase SpawnProjectile(EntityCharacter monster, EntityStats entityStats, Vector2 spawnPosition, Action action, string GameTag, RangedProjectileType type)
        {
            var projectile = Get(type);
            if (projectile == null) return null;
            projectile.transform.position = spawnPosition;
            projectile.RegisterOnEndVfx(action);
            projectile.Initialized(monster.transform, entityStats, GameTag);
            return projectile;
        }
    }
}