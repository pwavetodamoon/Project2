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
        [SerializeField] protected Animator_Base animator_Base;
        [SerializeField] protected EntityAttackControl attackControl;
        [SerializeField] protected IGetAttackerTransform attackerTransform;
        [SerializeField] protected EntityCombat attackManager;
        [SerializeField] protected EntityStats entityStats;
        [SerializeField] protected EntityTakeDamage EntityTakeDamage;
        protected EntityTakeDamage entityTakeDamage;
        // Support for getting reference from children or parent
        [SerializeField] protected EntityReferences EntityReferences;
        protected virtual void Awake()
        {
            animator_Base = EntityReferences.GetRef<Animator_Base>();
            attackControl = EntityReferences.GetRef<EntityAttackControl>();
            attackerTransform = EntityReferences.GetRef<IGetAttackerTransform>();
            attackManager = EntityReferences.GetRef<EntityCombat>();
            entityStats = EntityReferences.GetRef<EntityStats>();
            EntityTakeDamage = EntityReferences.GetRef<EntityTakeDamage>();

        }
        public bool EntityInAttackState() => attackControl.IsAttacking();
        public Animator_Base GetAnimatorBase()  => animator_Base;
        public Transform GetAttackerTransform() => attackerTransform.GetAttackerTransform();

        public EntityCombat GetEntityCombat() => attackManager;

        public EntityStats GetEntityStats() => entityStats;
        public EntityTakeDamage GetEntityTakeDamage() => EntityTakeDamage;


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
            if (attackManager == null)
            {
                attackManager = GetComponent<EntityCombat>();
            }
            attackManager.SetAllowExecuteAttackValue(state);
            attackManager.SetTimeCounterValue(state);
        }
        public void StopExecute()
        {
            attackControl.StopExecute();
        }
    }
}