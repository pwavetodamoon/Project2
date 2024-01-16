using System;
using System.Collections;
using Characters.Monsters;
using NewCombat.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat
{
    public abstract class BaseHeroNormalAttack : MonoBehaviour
    {
        [Header("References/BaseClass")]
        protected Animator_Base animator;
        public Transform gizmosTransform;

        [Header("Vector2/BaseClass")]
        public Vector2 gizmosPosition;
        public Vector2 size = Vector3.one;

        [Header("Value/BaseClass")]
        public float Angle = 0;
        public float Speed = 2;
        public bool IsActive = false;

        [Header("Features")]
        [ShowInInspector] protected AttackCounter attackCounter = new();

        private void Update()
        {
            attackCounter.CheckTimerCounter(IsActive,Time.deltaTime);
        }

        private void OnEnable()
        {
            attackCounter.AttackAction += ExecuteAttack;
        }

        private void OnDisable()
        {
            attackCounter.AttackAction -= ExecuteAttack;
        }
        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator_Base>();
        }
        protected virtual HeroCharacter GetCharacter() => GetComponentInParent<HeroCharacter>();
        protected virtual IEnumerator StartBehavior(HeroCharacter hero) { yield return null; }

        protected virtual void OnDrawGizmos() { }

        // Check the collider in the gizmo
        [Button]
        protected virtual void CheckCollider() { }
        // Execute the attack, this is interface method
        public void ExecuteAttack()
        {
            if (IsActive) return;
            Debug.Log("Excute attack");
            IsActive = true;
            StartCoroutine(StartBehavior(GetCharacter()));
        }
    }
}

