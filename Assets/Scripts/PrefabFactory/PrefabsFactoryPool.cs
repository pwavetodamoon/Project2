using Helper;
using NewCombat.Projectiles;
using ObjectPool;
using UnityEngine;

namespace PrefabFactory
{
    public enum ProjectileType
    {
        Arrow,
        Magic1,
        FireBall
    } 
    public class PrefabsFactoryPool : Singleton<PrefabsFactoryPool>
    {
        // Use in Scene Environment
        public Projectile ProjectilePrefab;
        public MagicProjectile MagicProjectilePrefab;
        public ObjectPoolPrefab<ProjectileBase> projectile_pool;
        public ObjectPoolPrefab<ProjectileBase> magicProjectile_pool;

        public ProjectileBase Get(ProjectileType type)
        {
            switch (type)
            {
                case ProjectileType.Arrow:
                    return projectile_pool.Get();
                case ProjectileType.Magic1:
                    return magicProjectile_pool.Get();
            }
            return null;
        }

        protected override void Awake()
        {
            base.Awake();
            var go = new GameObject();
            go.transform.position = Vector2.zero;
            go.name = "PoolHolder";
            projectile_pool = new ObjectPoolPrefab<ProjectileBase>(ProjectilePrefab,go.transform,5);
            magicProjectile_pool = new ObjectPoolPrefab<ProjectileBase>(MagicProjectilePrefab,go.transform,5);
        }
    
    }
}
