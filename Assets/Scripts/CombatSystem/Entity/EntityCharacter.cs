using CombatSystem.Attack.Systems;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using LevelAndStats;
using UnityEngine;

namespace CombatSystem.Entity
{
    public abstract class EntityCharacter : MonoBehaviour, IEntity
    {
        protected AttackControl attackControl;
        protected IGetAttackerTransform attackerTransform;
        protected AttackManager attackManager;


        protected virtual void Awake()
        {
            attackControl = GetComponent<AttackControl>();
            attackerTransform = GetComponent<IGetAttackerTransform>();
            attackManager = GetComponent<AttackManager>();
        }

        public void SetAttackState(bool state)
        {
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
    }
}