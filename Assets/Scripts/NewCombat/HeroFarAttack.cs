using Characters;
using NewCombat.HeroAttack;
using NewCombat.Projectiles;
using System.Collections;
using Helper;
using NewCombat.Characters;
using NewCombat.ManagerInEntity;
using PrefabFactory;
using UnityEngine;

namespace NewCombat
{
    public class HeroFarAttack : BaseHeroAttack
    {
        private ProjectileType type;
        private WaitUntil waitUntilCanCauseDamage;
        private WaitForSeconds waitForEndAnim;

        
        private bool IsProjectileHitEnemy = false;



        protected override IEnumerator StartBehavior()
        {
            yield return base.StartBehavior();
            PlayAnimation(Human_Animator.Slash_State);

            yield return waitForEndAnim;

            var projectile = SpawnProjectile(Enemy);
            if (projectile == null)
            {
                yield break;
            }

            yield return waitUntilCanCauseDamage;

            CauseDamage();
            IsProjectileHitEnemy = false;
        }

        protected ProjectileBase SpawnProjectile(EntityCharacter monster)
        {
            var projectile = PrefabsFactoryPool.Instance.Get(type);
            projectile.transform.position = AttackTransform.position;
            projectile.RegisterOnEndVfx(AllowGoNextStep);
            projectile.Initialized(monster.transform, GameTag.Enemy);
            return projectile;
        }

        private void AllowGoNextStep()
        {
            IsProjectileHitEnemy = true;
        }

        public override void GetReference(EntityCharacter newEntityCharacter, AnimationManager _animationManager, AttackManager _attackManager,
            Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, _animationManager, _attackManager, attackTransform);

            waitUntilCanCauseDamage = new WaitUntil(() => IsProjectileHitEnemy);
            waitForEndAnim = new WaitForSeconds(GetAnimationLength(Human_Animator.Slash_State) / 2);
        }

        public HeroFarAttack(ProjectileType type)
        {
            this.type = type;
        }
    }
}