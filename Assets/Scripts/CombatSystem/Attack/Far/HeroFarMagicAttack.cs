using CombatSystem.Attack.Projectiles;
using CombatSystem.Attack.Utilities;
using Helper;
using Model.Hero;

namespace CombatSystem.Attack.Factory
{
    public class HeroFarMagicAttack : FarAttack
    {
        public HeroFarMagicAttack(RangedProjectileType type) : base(type)
        {
        }

        protected override ProjectileBase GetProjectile()
        {
            return PrefabAttackFactoryPool.Instance.SpawnProjectile(Enemy, MagicAttackTransform, AllowGoNextStep, GameTag.Enemy, type);
        }
        protected override void PlayAnimationAttack()
        {
            AudioManager.Instance.PlaySFX("Far Attack");
            PlayAnimation(AnimationType.Spell);
        }

    }
}
