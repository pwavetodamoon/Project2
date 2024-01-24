using System.Collections;
using Characters.Monsters;
using CombatSystem;
using NewCombat.Characters;
using UnityEngine;
namespace NewCombat.MonsterAttack
{
    public class MonsterNearAttack : BaseNearAttack
    {
        public MonsterNearAttack(EntityCharacter newEntityCharacter, Transform attackTransform = null) : base(newEntityCharacter, attackTransform)
        {
        }
        private void Update()
        {

        }
        protected override IEnumerator StartBehavior()
        {
            var hero = CombatManager.Instance.GetHero();
            if (hero == null)
            {
                IsActive = false;
                yield break;
            }
            var attackTime = animator.GetAnimationLength(Monster_Animator.Attack_State);
            animator.ChangeAnimation(Monster_Animator.Attack_State);
            yield return new WaitForSeconds(attackTime);
            CauseDamage(GameTag.Hero);
            ResetStateAndCounter();
        }
    }
}
