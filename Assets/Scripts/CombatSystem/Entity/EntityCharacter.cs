using CombatSystem.Attack.Systems;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using LevelAndStats;
using Model.Monsters;
using UnityEngine;

namespace CombatSystem.Entity
{
    //[RequireComponent(typeof(EntityReferences))]
    public abstract class EntityCharacter : MonoBehaviour, IEntity
    {
        [SerializeField] protected Animator_Base animator_Base;
        [SerializeField] protected EntityAttackControl attackControl;
        [SerializeField] protected IGetAttackerTransform attackerTransform;
        [SerializeField] protected EntityCombat entityCombat;
        [SerializeField] protected EntityStats entityStats;
        [SerializeField] protected EntityTakeDamage EntityTakeDamage;
        [SerializeField] protected EntityAction entityAction;
        // Support for getting reference from children or parent

        protected virtual void Awake()
        {
            //animator_Base = EntityReferences.GetRef<Animator_Base>();
            //attackControl = EntityReferences.GetRef<EntityAttackControl>();
            //attackerTransform = EntityReferences.GetRef<IGetAttackerTransform>();
            //entityCombat = EntityReferences.GetRef<EntityCombat>();
            //entityStats = EntityReferences.GetRef<EntityStats>();
            //EntityTakeDamage = EntityReferences.GetRef<EntityTakeDamage>();
            //entityAction = EntityReferences.GetRef<EntityAction>();

            entityCombat = GetComponentInChildren<EntityCombat>();
            entityStats = GetComponentInChildren<EntityStats>();
            EntityTakeDamage = GetComponentInChildren<EntityTakeDamage>();
            entityAction = GetComponentInChildren<EntityAction>();
            animator_Base = GetComponentInChildren<Animator_Base>();
            attackControl = GetComponentInChildren<EntityAttackControl>();
            attackerTransform = GetComponentInChildren<IGetAttackerTransform>();

        }

        public bool EntityInAttackState() => attackControl.IsAttacking();

        //public Animator_Base GetAnimatorBase() => animator_Base;

        public Transform GetAttackerTransform() => attackerTransform.GetAttackerTransform();

        //public EntityCombat GetEntityCombat() => entityCombat;

        //public EntityStats GetEntityStats() => entityStats;

        //public EntityTakeDamage GetEntityTakeDamage() => EntityTakeDamage;
        //public EntityAction GetEntityAction() => entityAction;

        public virtual void RegisterObject()
        {
            SetAttackState(true);
        }

        public virtual void ReleaseObject()
        {
            SetAttackState(false);
        }

        public void SetAttackState(bool state)
        {
            if (entityCombat == null)
            {
                entityCombat = GetComponentInChildren<EntityCombat>();
            }
            entityCombat.SetAllowExecuteAttackValue(state);
            entityCombat.SetTimeCounterValue(state);
        }

        public void StopExecute()
        {
            attackControl.StopExecute();
        }
        public T GetRef<T>()
        {
            var t = GetComponentInChildren<T>();
            if(t == null)
            {
                t = GetComponentInParent<T>();
            }
            return t;
        }
    }
}