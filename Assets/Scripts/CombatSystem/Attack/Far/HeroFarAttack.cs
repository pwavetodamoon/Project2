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
    public class HeroFarAttack : BaseHeroAttack
    {
        private readonly ProjectileType type;
        private bool IsProjectileHitEnemy;
        private WaitForSeconds waitForEndAnim;
        private WaitUntil waitUntilCanCauseDamage;

        public HeroFarAttack(ProjectileType type)
        {
            this.type = type;
        }


        protected override IEnumerator StartBehavior()
        {
            yield return base.StartBehavior();
            PlayAnimation(Human_Animator.Slash_State);

            yield return waitForEndAnim;

            var projectile = SpawnProjectile(Enemy);
            if (projectile == null) yield break;

            yield return waitUntilCanCauseDamage;

            CauseDamage();
            IsProjectileHitEnemy = false;
        }

        protected ProjectileBase SpawnProjectile(EntityCharacter monster)
        {
            var projectile = PrefabAttackFactoryPool.Instance.Get(type);
            projectile.transform.position = AttackTransform.position;
            projectile.RegisterOnEndVfx(AllowGoNextStep);
            projectile.Initialized(monster.transform, GameTag.Enemy);
            return projectile;
        }

        private void AllowGoNextStep()
        {
            IsProjectileHitEnemy = true;
        }

        public override void GetReference(EntityCharacter newEntityCharacter, AnimationManager _animationManager,
            AttackManager _attackManager,
            Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, _animationManager, _attackManager, attackTransform);

            waitUntilCanCauseDamage = new WaitUntil(() => IsProjectileHitEnemy);
            waitForEndAnim = new WaitForSeconds(GetAnimationLength(Human_Animator.Slash_State) / 2);
        }
    }
}