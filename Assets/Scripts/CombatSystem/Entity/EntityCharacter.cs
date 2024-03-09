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

        protected virtual void Awake()
        {
            attackControl = GetComponent<AttackControl>();
            attackerTransform = GetComponent<IGetAttackerTransform>();
        }

        public abstract void RegisterObject();

        public abstract void ReleaseObject();
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