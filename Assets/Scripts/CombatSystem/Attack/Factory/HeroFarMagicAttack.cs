using System.Collections;
using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Projectiles;
using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using Helper;
using Model.Hero;
using UnityEngine;

namespace CombatSystem.Attack.Factory
{
    public class HeroFarMagicAttack : BaseSingleTargetAttack
    {
        private readonly RangedProjectile type;
        private bool IsProjectileHitEnemy;
        private WaitForSeconds waitForEndAnim;
        private WaitUntil waitUntilCanCauseDamage;
        private WaitUntil WaitForInvokeAction;
        private ShootDetect shootDetect;

        public HeroFarMagicAttack(RangedProjectile type)
        {
            this.type = type;
        }

        private void SetupYieldInstruction()
        {
            WaitForInvokeAction = new WaitUntil(() => shootDetect.ShootDone);
            waitUntilCanCauseDamage = new WaitUntil(() => IsProjectileHitEnemy);
            waitForEndAnim = new WaitForSeconds(GetAnimationLength(AnimationType.Attack) / 2);
        }
        protected override IEnumerator StartBehavior()
        {
            PlayAnimationAttack();
            yield return WaitForInvokeAction;
            if (Enemy == null) yield break;
            if (GetProjectile() != null)
            {
                yield return waitUntilCanCauseDamage;
                CauseDamage();
            }
        }
        protected virtual ProjectileBase GetProjectile()
        {
            return PrefabAttackFactoryPool.Instance.SpawnProjectile(Enemy, MagicAttackTransform, AllowGoNextStep, GameTag.Enemy, type);
        }
        protected virtual void PlayAnimationAttack()
        {
            AudioManager.Instance.PlaySFX("Far Attack");
            PlayAnimation(AnimationType.Attack);

        }
        protected override void ResetStateAndCounter()
        {
            base.ResetStateAndCounter();
            IsProjectileHitEnemy = false;
            shootDetect.ResetShoot();
        }
        protected override string GetEnemyTag() => GameTag.Enemy;

        private void AllowGoNextStep() => IsProjectileHitEnemy = true;

        public override void GetReference(EntityCharacter newEntityCharacter,
            Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, attackTransform);
            SetupYieldInstruction();
            shootDetect = animator.GetComponent<ShootDetect>();
        }

    }
}
