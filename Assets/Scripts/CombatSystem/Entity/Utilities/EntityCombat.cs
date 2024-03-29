using CombatSystem.Attack.Systems;
using CombatSystem.Attack.Utilities;
using LevelAndStats;
using Model.Hero;
using Model.Monsters;
using UnityEditor;
using UnityEngine;

namespace CombatSystem.Entity.Utilities
{
    public class EntityCombat : MonoBehaviour, IAttackerCounter
    {
        private Animator_Base animator_base;
        [SerializeField] private EntityCharacter entity;
        private EntityHelper EntityHelper;
        private EntityStats entityStats;
        [SerializeField] protected bool allowCounter = true;
        [SerializeField] protected bool allowExecuteAnotherAttack = true;
        public int Count { get; set; }
        public Transform parent;
        private void Start()
        {
            entity = GetComponentInParent<EntityCharacter>();
            entityStats = parent.GetComponentInChildren<EntityStats>();
            animator_base = parent.GetComponentInChildren<Animator_Base>();
            animator_base = entity.GetAnimatorBase();
            EntityHelper = new EntityHelper(entityStats);
        }

        private bool CanChangeAnimation() => entity != null
        && entity.EntityInAttackState() == false
        && entityStats.Health() > 0;

        public bool AttackedByEnemies() => Count > 0;

        public void DecreaseAttackerCount(EntityStats entity1)
        {
            if(EntityHelper.EntityStats == null)
            {
                Debug.Log("EntityStats is null",gameObject);
            }
            EntityHelper.Remove(entity1);
            if (--Count < 0 && CanChangeAnimation())
            {
                animator_base.ChangeAnimation(AnimationType.Walk);
            }
        }

        public void IncreaseAttackerCount(EntityStats entity1)
        {
            Debug.Log(transform.name + " IncreaseAttackerCount: " + EntityHelper.sumOfDamage);
            EntityHelper.Add(entity1);
            if (CanChangeAnimation())
            {
                animator_base.ChangeAnimation(AnimationType.Idle);
            }
            ++Count;
        }

        public bool IsAllowAttack() => allowExecuteAnotherAttack;

        public bool IsAllowCounter() => allowCounter;

        public bool IsOutOfHealth()
        {
            if (entityStats == null)
            {
                entityStats = entity.GetComponentInChildren<EntityStats>();
                EntityHelper = new EntityHelper(entityStats);
                Debug.Log("is null");
            }
            return EntityHelper.sumOfDamage >= entityStats.Health();
        }

        public void SetAllowExecuteAttackValue(bool value) => allowExecuteAnotherAttack = value;

        public bool SetTimeCounterValue(bool value) => allowCounter = value;
    }
}