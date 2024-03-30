using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Systems;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using Helper;
using LevelAndStats;
using Model.Hero;
using Model.Monsters;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CombatSystem.Attack.Near
{
    public class MonsterNearAttack : BaseSingleTargetAttack
    {

        public EntityAttackControl EntityAttackControl;
        public Animator_Base Animator;

        private void Awake()
        {
            
        }
        protected override IEnumerator StartBehavior()
        {
            //if (IsOnTarget() == false || EntityStats.Health() < 0) yield break;
            PlayAnimation(AnimationType.Attack);
            yield return new WaitForSeconds(GetAnimationLength(AnimationType.Attack));
            CauseDamage();

            //PlayAnimation(IsOnTarget() ? AnimationType.Idle : AnimationType.Walk);
        }

        private void CauseDamage()
        {
            throw new NotImplementedException();
        }

        private void PlayAnimation(AnimationType attack)
        {
            Animator?.ChangeAnimation(attack);
        }

        private float GetAnimationLength(AnimationType attack)
        {
            if (EntityAttackControl == null) return 0;
            return Animator.GetAnimationLength(AnimationType.Attack);
        }


        // protected override string GetEnemyTag()
        // {
        //     return GameTag.Hero;
        // }

    }
}