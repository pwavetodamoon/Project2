using System.Collections;
using Characters;
using CombatSystem;
using NewCombat.Characters;
using UnityEngine;

namespace NewCombat.HeroAttack
{
    public class HeroFarAttack : BaseHeroNormalAttack
    {
        // FIXME: use check null to much
        public GameObject projectilePrefab;
        public float coolDownTime = 0.5f;
        public HeroCharacter hero;
        public Transform shotterTransform;
        protected override void Awake()
        {
            base.Awake();
            hero = GetComponent<HeroCharacter>();
        }
        protected override IEnumerator StartBehavior()
        {
            MonsterCharacter monster = CombatManager.Instance.GetMonster();

            if (monster == null)
            {
                Debug.Log("Target is null");
                IsActive = false;
                yield break;
            }

            hero.allowExcuteAnotherAttack = false;
            yield return Fire();
            yield return new WaitForSeconds(coolDownTime);
            ResetStateAndCounter();
        }
        IEnumerator Fire()
        {
            animator.ChangeAnimation(Human_Animator.Slash_State);

            var time = animator.GetAnimationLength(Human_Animator.Slash_State);

            yield return new WaitForSeconds(time / 2);

            SpawnProjectile();
        }
        protected Transform SpawnProjectile()
        {
            var monster = CombatManager.Instance.GetMonster();
            if (monster == null) { return null; }

            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            projectile.transform.position = shotterTransform.position;
            projectile.GetComponent<Projectile>().Initialized(hero, monster.transform, "Enemy");
            return projectile.transform;
        }
    }
}
