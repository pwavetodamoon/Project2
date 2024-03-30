using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Systems;
using CombatSystem.Attack.Utilities;
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
        public EntityStats EntityStats;
        public EntityCharacter EntityCharacter;
        private void Awake()
        {
            EntityCharacter = GetComponentInParent<EntityCharacter>();
            EntityAttackControl = EntityCharacter.GetRef<EntityAttackControl>();
            EntityAttackControl.OnAttack += RunATtack;
            EntityStats = EntityCharacter.GetRef<EntityStats>();
            Animator = EntityCharacter.GetRef<Animator_Base>();
        }
        private void RunATtack(EntityCharacter entity)
        {
            EntityAttackControl.StartCoroutine(StartBehavior(entity));
        }
        protected IEnumerator StartBehavior(EntityCharacter entity)
        {
            //if (IsOnTarget() == false || EntityStats.Health() < 0) yield break;
            //if(CheckBeforeAttack(entity) == false)
            //{
            //    yield break;
            //}

            PlayAnimation(AnimationType.Attack);
            yield return new WaitForSeconds(GetAnimationLength(AnimationType.Attack));
            if(entity == null)
            {
                yield break;
            }
            CauseDamage(entity);
            EntityAttackControl.ResetAtackState();
            //PlayAnimation(IsOnTarget() ? AnimationType.Idle : AnimationType.Walk);
        }

        private bool CheckBeforeAttack(EntityCharacter entity)
        {
            var entityCombat = entity.GetRef<EntityCombat>();
            if (entityCombat != null)
            {
                return entityCombat.Check(this.EntityStats, GameTag.Hero);
            }
            return false;
        }

        private void CauseDamage(EntityCharacter entity)
        {
            var damageable = entity.GetRef<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(this.EntityStats);
            }
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