using Leveling_System;
using NewCombat.Helper;
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
        protected IGetAttackerTransform attackerTransform;
        protected virtual void Awake()
        {
            attackManager = GetComponent<AttackManager>();
            attackControl = GetComponent<AttackControl>();
            animationManager = GetComponent<AnimationManager>();
            attackerTransform = GetComponent<IGetAttackerTransform>();
        }

        public abstract void RegisterObject();

        public abstract void ReleaseObject();

        public virtual void PlayHurtAnimation()
        {
        }

        public Transform GetAttackerTransform()
        {
            return attackerTransform.GetAttackerTransform();
        }
        public bool EntityInAttackState()
        {
            return attackControl.IsAttacking();
        }
    }
}