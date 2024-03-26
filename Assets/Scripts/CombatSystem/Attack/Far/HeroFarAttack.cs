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
            var go = PrefabAttackFactoryPool.Instance.SpawnProjectile(Enemy, MagicAttackTransform, AllowGoNextStep, GameTag.Enemy, type);
            counter++;

            if (counter > 2)
            {
                counter = 0;
                go.GetComponent<Projectile>().useVfx = true;
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