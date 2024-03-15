using CombatSystem.Attack.Systems;
using CombatSystem.Attack.Utilities;
using LevelAndStats;
using Model.Hero;
using Model.Monsters;
using UnityEditor;
using UnityEngine;

namespace CombatSystem.Entity.Utilities
{
    public class AttackManager : MonoBehaviour, IAttackerCounter
    {
        [SerializeField] protected bool allowCounter = true;
        [SerializeField] protected bool allowExecuteAnotherAttack = true;
        private Animator_Base animator_base;
        private IEntity entity;
        private EntityHelper EntityHelper;
        private EntityStats entityStats;
        public int Count { get; set; }

        private void Awake()
        {
            animator_base = GetComponentInChildren<Animator_Base>();
            entity = GetComponent<IEntity>();
            entityStats = GetComponent<EntityStats>();
            EntityHelper = new EntityHelper(GetComponent<EntityStats>());
        }


        public void IncreaseAttackerCount(EntityStats entity1)
        {
            EntityHelper.Add(entity1);
            if (CanChangeAnimation())
            {
                animator_base.ChangeAnimation(AnimationType.Idle);
            }
            ++Count;
        }

        public void DecreaseAttackerCount(EntityStats entity1)
        {
            EntityHelper.Remove(entity1);
            if (--Count < 0 && CanChangeAnimation())
            {
                animator_base.ChangeAnimation(AnimationType.Walk);
            }
        }

        public bool CanAttack()
        {
            if (EntityHelper.sumOfDamage >= entityStats.Health())
            {
                return false;
            }
            return true;
        }

        private bool CanChangeAnimation()
        {
            return entity != null && entity.EntityInAttackState() == false;
        }
        public bool AttackedByEnemies()
        {
            return Count > 0;
        }

        public bool IsAllowAttack()
        {
            return allowExecuteAnotherAttack;
        }

        public bool IsAllowCounter()
        {
            return allowCounter;
        }

        public bool SetAllowExecuteAttackValue(bool value)
        {
            return allowExecuteAnotherAttack = value;
        }

        public bool SetTimeCounterValue(bool value)
        {
            return allowCounter = value;
        }
    }
}