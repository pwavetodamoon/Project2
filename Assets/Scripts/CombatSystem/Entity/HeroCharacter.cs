using CombatSystem.Attack.Factory;
using CombatSystem.Entity.Utilities;
using CombatSystem.HeroDataManager.Data;
using Helper;
using LevelAndStats;
using Model.Hero;
using Model.Monsters;
using SlotHero;
using UnityEngine;

namespace CombatSystem.Entity
{
    public class HeroCharacter : EntityCharacter
    {
        public CharacterEnumType characterEnumType;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private HeroSingleAttackFactory attackFactory;

        private EntityStateManager entityStateManager;
        private Character_Body_Sprites sprites;
        private Animator_Base animatorBase;
        private AttackManager attackManager;
        public bool IsDead;

        public int InGameSlotIndex { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            entityStateManager = GetComponent<EntityStateManager>();
            attackManager = GetComponent<AttackManager>();
            gameObject.layer = LayerMask.NameToLayer(GameLayerMask.Hero);
            sprites = GetComponentInChildren<Character_Body_Sprites>();
            animatorBase = GetComponentInChildren<Animator_Base>();

            entityStateManager.OnDie += OnDead;
            entityStateManager.OnRebirth += OnRebirth;
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
            animatorBase.DisableAnimator();
            sprites.SetDeadSprite();
        }

        private void OnRebirth()
        {
            animatorBase.EnableAnimator();
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