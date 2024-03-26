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
        public HeroFarAttack(RangedProjectileType type) : base(type)
        {
        }

        protected override ProjectileBase GetProjectile()
        {
            return PrefabAttackFactoryPool.Instance.SpawnProjectile(Enemy, EntityStats, MagicAttackTransform, AllowGoNextStep, GameTag.Enemy, type);
        }
        protected override void PlayAnimationAttack()
        {
            AudioManager.Instance.PlaySFX("Far Attack");
            PlayAnimation(AnimationType.Shooting);
        }
    }
}