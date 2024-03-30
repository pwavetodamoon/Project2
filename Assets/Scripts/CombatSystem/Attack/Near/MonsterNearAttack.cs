using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Systems;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using CombatSystem.MonsterAI;
using Helper;
using LevelAndStats;
using Model.Hero;
using Model.Monsters;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CombatSystem.Attack.Near
{
    public class MonsterNearAttack : BaseSingleTargetAttack
    {


        private MonsterMoveAI MonsterMoveAI;

        public override void GetReference(EntityCharacter newEntityCharacter, Transform attackTransform = null)
        {
            base.GetReference(newEntityCharacter, attackTransform);
            PlayAnimation(AnimationType.Walk);


            //entityCharacter.GetComponent<MonsterNearAI>().TriggerAttackEvent += MonsterMoveAI.EnableAttack;
            //attackControl.StartCoroutine(MonsterMoveAI.MoveBehaviour());
        }

        protected override IEnumerator StartBehavior()
        {
            //if (IsOnTarget() == false || EntityStats.Health() < 0) yield break;
            PlayAnimation(AnimationType.Attack);
            yield return new WaitForSeconds(GetAnimationLength(AnimationType.Attack));
            CauseDamage();

            //PlayAnimation(IsOnTarget() ? AnimationType.Idle : AnimationType.Walk);
        }


        protected override string GetEnemyTag()
        {
            return GameTag.Hero;
        }

    }
}