using CombatSystem;
using Helper;
using LevelAndStats;
using Model.Hero;
using NewCombat.Attack.Factory;
using NewCombat.Entity.Utilities;
using NewCombat.HeroDataManager.Data;
using SlotHero;
using UnityEngine;

namespace NewCombat.Entity
{
    public class HeroCharacter : EntityCharacter
    {
        public CharacterEnumType characterEnumType;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private HeroSingleAttackFactory attackFactory;
        public bool IsDead;

        private EntityStateManager entityStateManager;
        private Character_Body_Sprites sprites;
        public int InGameSlotIndex { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            entityStateManager = GetComponent<EntityStateManager>();
            gameObject.layer = LayerMask.NameToLayer(GameLayerMask.Hero);
            entityStateManager.OnDie += OnDead;
            entityStateManager.OnRebirth += OnRebirth;
            sprites = GetComponentInChildren<Character_Body_Sprites>();
        }

        private void OnDestroy()
        {
            entityStateManager.OnDie -= OnDead;
            entityStateManager.OnRebirth -= OnRebirth;
        }

        private void OnDead()
        {
            if (IsDead == false && CombatEntitiesManager.Instance.GetHeroCount() == 1)
            {
                GameLevelControl.Instance.LossTransitionHandler.UseRunner();
                Debug.Log("Thua roi");
            }

            SetDeadState();
        }

        public void SetDeadState()
        {
            SetModelBackImmediate();
            animationManager.DisableAnimator();
            sprites.SetDeadSprite();
        }

        private void OnRebirth()
        {
            animationManager.EnableAnimator();
            sprites.SetRebirthSprite();
            RegisterObject();
        }

        public void SetSlotIndex(int index)
        {
            if (index == -1)
                ReleaseObject();
            else
                RegisterObject();
            InGameSlotIndex = index;
        }

        public void SetModelBackImmediate()
        {
            modelTransform.transform.position = SlotManager.Instance.GetStandTransform(InGameSlotIndex).position;
        }

        public void SetHeroData(HeroData newHeroData)
        {
            GetComponent<HeroEntityStats>().SetHero(newHeroData);
        }

        public virtual void CreateAttack()
        {
            if (attackFactory == null) return;
            attackControl.Create(attackFactory);
        }

        public void SetAttackFactory(HeroSingleAttackFactory baseAttack)
        {
            attackFactory = baseAttack;
        }

        public override void PlayHurtAnimation()
        {
            animationManager.PlayAnimation(Human_Animator.Hurt_State);
        }

        public Transform GetModelTransform()
        {
            if (modelTransform == null)
            {
                Debug.LogError("Model transform is null");
                return null;
            }

            return modelTransform;
        }

        public override void RegisterObject()
        {
            CombatEntitiesManager.Instance.AppendEntityToListByTag(this, GameTag.Hero);
            CreateAttack();
            SetAttackState(true);
        }

        public override void ReleaseObject()
        {
            SetAttackState(false);
            StopCurrentAttack();
            CombatEntitiesManager.Instance.RemoveEntityByTag(this, GameTag.Hero);
            //gameObject.SetActive(false);
        }

        public void StopCurrentAttack()
        {
            attackControl.StopAllCoroutines();
        }

        public void SetAttackState(bool state)
        {
            attackManager.SetAllowExecuteAttackValue(state);
            attackManager.SetTimeCounterValue(state);
        }
    }
}