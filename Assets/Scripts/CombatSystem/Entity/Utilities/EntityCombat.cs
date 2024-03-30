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
        private void Start()
        {
            entity = GetComponentInParent<EntityCharacter>();
            entityStats = entity.GetRef<EntityStats>();
            animator_base = entity.GetRef<Animator_Base>();
            EntityHelper = new EntityHelper(entityStats);
        }

        private bool CanChangeAnimation() => entity != null
        && entity.EntityInAttackState() == false
        && entityStats.Health() > 0;

        public bool AttackedByEnemies() => Count > 0;

        public void DecreaseAttackerCount(EntityStats entity1)
        {
            if (EntityHelper.EntityStats == null)
            {
                Debug.Log("EntityStats is null", gameObject);
            }
            EntityHelper.Remove(entity1);
            if (--Count < 0 && CanChangeAnimation())
            {
                animator_base.ChangeAnimation(AnimationType.Walk);
            }
        }

        public void IncreaseAttackerCount(EntityStats entityStats)
        {
            EntityHelper.Add(entityStats);
            if (CanChangeAnimation())
            {
                animator_base.ChangeAnimation(AnimationType.Idle);
            }
            ++Count;
        }
        public void IncreaseAttacker(EntityStats entity1)
        {
            EntityHelper.Add(entity1);
            if (CanChangeAnimation())
            {
                animator_base.ChangeAnimation(AnimationType.Idle);
            }
            ++Count;
        }
        public bool Check(EntityStats entity1, string tag)
        {
            Debug.Log(transform.name + " Check: " + EntityHelper.sumOfDamage);
            if(IsOutOfHealth() && entityStats.Health() > 0)
            {
                IncreaseAttacker(entity1);
            }
            var enemy = entity1.GetComponentInParent<EntityCharacter>();
            if (IsOutOfHealth())
            {
                CombatEntitiesManager.Instance.RemoveEntityByTag(enemy, tag);
                return true;
            }


            return false;
        }

        public bool IsAllowAttack() => allowExecuteAnotherAttack;

        public bool IsAllowCounter() => allowCounter;

        public bool IsOutOfHealth()
        {
            if (entityStats == null)
            {
                //entityStats = entity.GetComponentInChildren<EntityStats>();
                //EntityHelper = new EntityHelper(entityStats);
                Debug.Log("is null");
            }
            return EntityHelper.sumOfDamage >= entityStats.Health();
        }

        public void SetAllowExecuteAttackValue(bool value) => allowExecuteAnotherAttack = value;

        public bool SetTimeCounterValue(bool value) => allowCounter = value;

    }
}