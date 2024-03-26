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
    public abstract class FarAttack : BaseSingleTargetAttack
    {
        protected readonly RangedProjectileType type;
        public FarAttack(RangedProjectileType type)
        {
            this.type = type;
        }
        private bool IsProjectileHitEnemy;
        private WaitForSeconds waitForEndAnim;
        private WaitUntil waitUntilCanCauseDamage;
        private WaitUntil WaitForInvokeAction;
        private ShootDetect shootDetect;


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
        protected abstract ProjectileBase GetProjectile();
        protected abstract void PlayAnimationAttack();
        protected override void ResetStateAndCounter()
        {
            base.ResetStateAndCounter();
            IsProjectileHitEnemy = false;
            shootDetect.ResetShoot();
        }
        protected override string GetEnemyTag() => GameTag.Enemy;

        protected void AllowGoNextStep() => IsProjectileHitEnemy = true;

        public override void GetReference(EntityCharacter newEntityCharacter,
            Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, attackTransform);
            SetupYieldInstruction();
            shootDetect = animator.GetComponent<ShootDetect>();
        }
    }
}
