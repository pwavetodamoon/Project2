using System.Collections;
using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Factory;
using CombatSystem.Attack.Projectiles;
using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using Helper;
using Model.Hero;
using UnityEngine;

namespace CombatSystem.Attack.Far
{
    public class HeroFarAttack : FarAttack
    {
        int counter = 0;
        public HeroFarAttack(RangedProjectileType type) : base(type)
        {
        }

        protected override ProjectileBase GetProjectile()
        {
            var go = PrefabAttackFactoryPool.Instance.SpawnProjectile(Enemy, EntityStats, BowAttackTransform.position, AllowGoNextStep, GameTag.Enemy, type);
            counter++;

            if (counter > 3)
            {
                counter = 0;
                var pos1 = BowAttackTransform.position + new Vector3(0, 1f);
                var pos2 = BowAttackTransform.position + new Vector3(0, -1f);
                PrefabAttackFactoryPool.Instance.SpawnProjectile(Enemy, EntityStats, pos1, null, GameTag.Enemy, type);
                PrefabAttackFactoryPool.Instance.SpawnProjectile(Enemy, EntityStats, pos2, null, GameTag.Enemy, type);
                // go.GetComponent<Projectile>().useVfx = true;
            }
            return go;
        }
        protected override void PlayAnimationAttack()
        {
            AudioManager.Instance.PlaySFX("Far Attack");
            PlayAnimation(AnimationType.Shooting);
        }
    }
}