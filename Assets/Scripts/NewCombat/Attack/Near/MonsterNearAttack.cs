using System.Collections;
using Model.Monsters;
using NewCombat.Attack.Abstracts;
using NewCombat.Entity;
using NewCombat.Entity.Utilities;
using NewCombat.MonsterAI;
using UnityEngine;

namespace NewCombat.Attack.Near
{
    public class MonsterNearAttack : BaseMonsterAttack
    {
        private MonsterNearAI MonsterNearAI;

        public override void GetReference(EntityCharacter newEntityCharacter, AnimationManager _animationManager,
            AttackManager attackManager, Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, _animationManager, attackManager, attackTransform);
            MonsterNearAI = entityCharacter.GetComponent<MonsterNearAI>();
        }

        protected override IEnumerator StartBehavior()
        {
            MonsterNearAI.SetEnemy(Enemy);
            if (!MonsterNearAI.CanAttack) yield break;

            var attackTime = GetAnimationLength(Monster_Animator.Attack_State);
            PlayAnimation(Monster_Animator.Attack_State);
            yield return new WaitForSeconds(attackTime);
            CauseDamage();
        }
    }
}