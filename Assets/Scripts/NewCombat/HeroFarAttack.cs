using Characters;
using NewCombat.HeroAttack;
using NewCombat.Projectiles;
using System.Collections;
using Helper;
using PrefabFactory;
using UnityEngine;

namespace NewCombat
{
    public class HeroFarAttack : BaseHeroAttack
    {
        private bool IsVfxEnd = false;

        protected override IEnumerator StartBehavior()
        {
            yield return base.StartBehavior();
            PlayAnimation(Human_Animator.Slash_State);
            var time = GetAnimationLength(Human_Animator.Slash_State);

            yield return new WaitForSeconds(time / 2);
            //var go = CombatEntitiesManager.Instance.GetEntityTransformByTag(entityCharacter.transform, GameTag.Enemy);

            var projectile = SpawnProjectile(Enemy);
            yield return new WaitUntil(() => IsVfxEnd);

            //yield return AttackEffectIEnumerator.ShakeCharacterMultiplierTimes(go.transform, .2f, 3);
            CauseDamage();
            IsVfxEnd = false;
        }

        protected ProjectileBase SpawnProjectile(GameObject monster)
        {
            var projectile = PrefabsFactoryPool.Instance.magicProjectile_pool.Get();
            //var projectile = PrefabsFactory.Instance.GetInstancePrefab(PrefabsFactory.MagicProjectile)
            //    .GetComponent<MagicProjectile>();
            projectile.RegisterOnEndVfx(AllowGoNextStep);
            projectile.transform.position = AttackTransform.position;
            projectile.Initialized(monster.transform, GameTag.Enemy);
            return projectile;
        }

        private void AllowGoNextStep()
        {
            IsVfxEnd = true;
        }
    }
}