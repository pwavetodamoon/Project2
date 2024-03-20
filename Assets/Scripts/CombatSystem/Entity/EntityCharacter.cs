using CombatSystem.Attack.Systems;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using LevelAndStats;
using Model.Monsters;
using UnityEngine;

namespace CombatSystem.Entity
{
    public abstract class EntityCharacter : MonoBehaviour, IEntity
    {
        protected AttackControl attackControl;
        protected IGetAttackerTransform attackerTransform;
        protected AttackManager attackManager;
        protected EntityStats entityStats;
        protected Animator_Base animator_Base;
        protected virtual void Awake()
        {
            attackControl = GetComponent<AttackControl>();
            attackerTransform = GetComponent<IGetAttackerTransform>();
            attackManager = GetComponent<AttackManager>();
            entityStats = GetComponent<EntityStats>();
            animator_Base = GetComponentInChildren<Animator_Base>();
        }
        public AttackManager GetAttackManager()
        {
            return attackManager;
        }
        public EntityStats GetEntityStats()
        {
            return entityStats;
        }
        public Animator_Base GetAnimatorBase()
        {
            return animator_Base;
        }
        public void SetAttackState(bool state)
        {
            if (attackManager == null)
            {
                attackManager = GetComponent<AttackManager>();
            }
            attackManager.SetAllowExecuteAttackValue(state);
            attackManager.SetTimeCounterValue(state);
        }
        public virtual void RegisterObject()
        {
            SetAttackState(true);
        }

        public virtual void ReleaseObject()
        {
            SetAttackState(false);
        }
        public bool EntityInAttackState()
        {
            return attackControl.IsAttacking();
        }

        public Transform GetAttackerTransform()
        {
            return attackerTransform.GetAttackerTransform();
        }

        public void StopExecute()
        {
            attackControl.StopExecute();
        }
    }
}