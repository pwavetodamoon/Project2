using CombatSystem.Attack.Systems;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using LevelAndStats;
using UnityEngine;

namespace CombatSystem.Entity
{
    [RequireComponent(typeof(AnimationManager))]
    [RequireComponent(typeof(AttackManager))]
    [RequireComponent(typeof(AttackControl))]
    [RequireComponent(typeof(EntityStateManager))]
    [RequireComponent(typeof(EntityStats))]
    public abstract class EntityCharacter : MonoBehaviour, IEntity
    {
        protected AnimationManager animationManager;
        protected AttackControl attackControl;
        protected IGetAttackerTransform attackerTransform;
        protected AttackManager attackManager;

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

        public bool EntityInAttackState()
        {
            return attackControl.IsAttacking();
        }

        public Transform GetAttackerTransform()
        {
            return attackerTransform.GetAttackerTransform();
        }
    }
}