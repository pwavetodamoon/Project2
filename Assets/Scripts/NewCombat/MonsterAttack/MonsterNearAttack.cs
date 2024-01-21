using System.Collections;
using Characters.Monsters;
using CombatSystem;
using NewCombat.Characters;
using NewCombat.HeroAttack;
using UnityEngine;

namespace NewCombat.MonsterAttack
{
    public class MonsterNearAttack : BaseNormalAttack
    {
        protected void OnDrawGizmos()
        {
            if (gizmosTransform == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(gizmosTransform.position, size);
        }

        protected override IEnumerator StartBehavior()
        {
            var hero = CombatManager.Instance.GetHero();
            if(hero == null)
            {
                IsActive = false;
                yield break;
            }
            var attackTime = animator.GetAnimationLength(Monster_Animator.Attack_State);
            animator.ChangeAnimation(Monster_Animator.Attack_State);
            yield return new WaitForSeconds(attackTime);
            
            CauseDamage("Hero");
            ResetStateAndCounter();
        }
    }
}
