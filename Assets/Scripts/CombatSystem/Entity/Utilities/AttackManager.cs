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
        private EntityCharacter entity;
        private EntityHelper EntityHelper;
        private EntityStats entityStats;
        public int Count { get; set; }

        private void Awake()
        {
            entity = GetComponent<EntityCharacter>();
            entityStats = entity.GetEntityStats();
            EntityHelper = new EntityHelper(entityStats);
            animator_base = entity.GetAnimatorBase();
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

        public bool IsOutOfHealth() => EntityHelper.sumOfDamage >= entityStats.Health();

        private bool CanChangeAnimation() => entity != null && entity.EntityInAttackState() == false;

        public bool AttackedByEnemies() => Count > 0;

        public bool IsAllowAttack() => allowExecuteAnotherAttack;


        public bool IsAllowCounter() => allowCounter;

        public void SetAllowExecuteAttackValue(bool value) => allowExecuteAnotherAttack = value;

        public bool SetTimeCounterValue(bool value) => allowCounter = value;

    }
}