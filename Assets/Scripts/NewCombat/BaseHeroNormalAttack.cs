using System.Collections;
using Characters.Monsters;
using NewCombat.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat
{
    public abstract class BaseHeroNormalAttack : MonoBehaviour
    {
        public bool isActive = false;
        public float speed = 2;
        protected Animator_Base animator;
        public Transform gizmosTransform;
        public Vector2 gizmosPosition;
        public Vector2 size = Vector3.one;
        public float angle = 0;
        [ShowInInspector] AttackCounter attackCounter = new();
        private void OnEnable()
        {
            attackCounter.CallbackEvent += ExecuteAttack;
        }
        private void OnDisable()
        {
            attackCounter.CallbackEvent -= ExecuteAttack;
        }
        private void Update()
        {
            attackCounter.CheckTimerCounter(this,Time.deltaTime);
        }
        private void Awake() => animator = GetComponentInChildren<Animator_Base>();
        protected virtual HeroCharacter GetCharacter() => GetComponentInParent<HeroCharacter>();
        protected virtual IEnumerator StartBehavior(HeroCharacter hero) { yield return null; }

        protected virtual void OnDrawGizmos() { }

        // Check the collider in the gizmo
        [Button]
        protected virtual void CheckCollider() { }
        // Execute the attack, this is interface method
        public void ExecuteAttack()
        {
            if (isActive) return;
            Debug.Log("Excute attack");
            isActive = true;
            StartCoroutine(StartBehavior(GetCharacter()));
        }
    }
}

