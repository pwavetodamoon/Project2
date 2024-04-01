using CombatSystem.Attack.Projectiles;
using CombatSystem.Attack.Utilities;
using Helper;
using Model.Hero;

namespace CombatSystem.Attack.Factory
{
    public class HeroFarMagicAttack : FarAttack
    {
        private int counter = 0;

        public HeroFarMagicAttack(RangedProjectileType type) : base(type)
        {
        }

        protected override ProjectileBase GetProjectile()
        {
            var go = PrefabAttackFactoryPool.Instance.SpawnProjectile(Enemy, EntityStats, MagicAttackTransform.position, AllowGoNextStep, GameTag.Enemy, type);
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
            PlayAnimation(AnimationType.Spell);
        }
    }
}