using System.Collections;
using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Projectiles;
using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using Helper;
using Model.Hero;
using UnityEngine;

namespace CombatSystem.Attack.Far
{
    public class HeroFarAttack : BaseSingleTargetAttack
    {
        private readonly RangedProjectileEnum type;
        private bool IsProjectileHitEnemy;
        private WaitForSeconds waitForEndAnim;
        private WaitUntil waitUntilCanCauseDamage;
        private WaitUntil waitUntilShootDone;
        private ShootDetect shootDetect;

        public HeroFarAttack(RangedProjectileEnum type)
        {
            this.type = type;
        }


        protected override IEnumerator StartBehavior()
        {
            AudioManager.Instance.PlaySFX("Far Attack");
            var attackTransform = BowAttackTransform;
            if (type == RangedProjectileEnum.Arrow)
            {
                PlayAnimation(AnimationType.Shooting);
            }
            else
            {
                PlayAnimation(AnimationType.Attack);
                attackTransform = MagicAttackTransform;
            }

            yield return waitUntilShootDone;
            if (Enemy == null) yield break;
            var projectile = SpawnProjectile(Enemy, attackTransform);
            if (projectile == null) yield break;
            yield return waitUntilCanCauseDamage;
            CauseDamage();
            IsProjectileHitEnemy = false;
            shootDetect.ResetShoot();
        }

        protected override string GetEnemyTag()
        {
            return GameTag.Enemy;
        }

        protected ProjectileBase SpawnProjectile(EntityCharacter monster, Transform attackTransform)
        {
            var projectile = PrefabAttackFactoryPool.Instance.Get(type);
            projectile.transform.position = attackTransform.position;
            projectile.RegisterOnEndVfx(AllowGoNextStep);
            projectile.Initialized(monster.transform, GameTag.Enemy);
            return projectile;
        }

        private void AllowGoNextStep()
        {
            IsProjectileHitEnemy = true;
        }

        public override void GetReference(EntityCharacter newEntityCharacter,
            Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, attackTransform);
            waitUntilShootDone = new WaitUntil(() => shootDetect.ShootDone);
            waitUntilCanCauseDamage = new WaitUntil(() => IsProjectileHitEnemy);
            shootDetect = animator.GetComponent<ShootDetect>();
            waitForEndAnim = new WaitForSeconds(GetAnimationLength(AnimationType.Attack) / 2);
        }
    }
}