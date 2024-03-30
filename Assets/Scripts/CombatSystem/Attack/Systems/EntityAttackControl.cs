using System;
using CombatSystem.Attack.Abstracts;
using CombatSystem.Attack.Factory;
using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using LevelAndStats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem.Attack.Systems
{
    public class EntityAttackControl : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Transform bowAttackTransform;
        [SerializeField] private Transform magicAttackTransform;
        [ShowInInspector] public BaseSingleTargetAttack attack;
        [SerializeField] public EntityCharacter entityCharacter;
        public event Action<EntityCharacter> OnAttack;
        public event Action OnEndAttack;
        private void Start()
        {
            entityCharacter = GetComponentInParent<EntityCharacter>();
        }

        [Button]
        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private bool CanNotRunAttackTimer()
        {
            return entityCharacter == null || attack == null;
        }


        public void Create(SingleAttackFactory factory)
        {
            InitAttackControl(factory.CreateAttack());
        }

        private void InitAttackControl(BaseSingleTargetAttack newAttack)
        {
            // if (entityCharacter == null)
            // {
            //     Debug.LogError("EntityCharacter is null");
            //     Debug.Log("Please fix it");
            // }
            // newAttack.GetReference(entityCharacter);

            // attack = newAttack;

            // attack.SetAttackTransform(bowAttackTransform, magicAttackTransform);

        }

        public bool IsAttacking()
        {
            if (attack == null) return false;
            // return attack.IsActive;
            return true;
        }

        [Button]
        public void StopExecute()
        {
            // Debug.Log("Stop Execute");
            StopAllCoroutines();
        }

        public void Attack(EntityCharacter entity)
        {
            if (entity == null)
            {
                Debug.Log("The Target is null");
                return;
            }
            OnAttack?.Invoke(entity);
        }
        public void ResetAtackState()
        {
            OnEndAttack?.Invoke();
        }
    }
}