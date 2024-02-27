using Leveling_System;
using NewCombat.ManagerInEntity;
using UnityEngine;

namespace NewCombat.Characters
{
    [RequireComponent(typeof(AnimationManager))]
    [RequireComponent(typeof(AttackManager))]
    [RequireComponent(typeof(AttackControl))]
    [RequireComponent(typeof(DamageManager))]
    [RequireComponent(typeof(EntityStats))]
    public abstract class EntityCharacter : MonoBehaviour, IEntity
    {
        protected AnimationManager animationManager;
        protected AttackControl attackControl;
        protected AttackManager attackManager;
        protected virtual void Awake()
        {
            attackManager = GetComponent<AttackManager>();
            attackControl = GetComponent<AttackControl>();
            animationManager = GetComponent<AnimationManager>();
        }

        public abstract void RegisterObject();

        public abstract void ReleaseObject();

        public virtual void PlayHurtAnimation()
        {
        }

        public bool EntityAreNotInAttackState()
        {
            return attackControl.IsAttacking() == false;
        }
    }
}