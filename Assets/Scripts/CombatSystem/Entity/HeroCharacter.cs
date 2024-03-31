using CombatSystem.Attack.Factory;
using CombatSystem.Entity.Utilities;
using CombatSystem.HeroDataManager.Data;
using Helper;
using LevelAndStats;
using Model.Hero;
using Sirenix.OdinInspector;
using SlotHero;
using SortingLayers;
using UnityEngine;

namespace CombatSystem.Entity
{
    public class HeroCharacter : EntityCharacter
    {
        public CharacterEnumType characterEnumType;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private HeroSingleAttackFactory attackFactory;

        [SerializeField] private Character_Body_Sprites sprites;
        [SerializeField] private SortingLayerByYAxis sortingLayerByYAxis;

        public SortingLayerByYAxis SortingLayerByYAxis => sortingLayerByYAxis;
        public int InGameSlotIndex { get; private set; }

        public bool IsDead;

        private void Start()
        {
            gameObject.layer = LayerMask.NameToLayer(GameLayerMask.HERO);
            entityAction.OnDie += OnDead;
            entityAction.OnRebirth += OnRebirth;
        }

        private void OnDead()
        {
            //if (IsDead == false && CombatEntitiesManager.Instance.GetHeroCount() == 1)
            //{
            //    GameLevelControl.Instance.OnLoose();
            //    Debug.Log("Thua roi");
            //}
            if(IsDead == false)
            {
                GameLevelControl.Instance.CheckOnLoose();
            }
            SetDeadState();
        }

        public void SetDeadState()
        {
            Debug.Log("Dead state");
            SetModelBackImmediate();
            animator_Base.ChangeAnimation(AnimationType.Dying);
            animator_Base.SetIsPlayDefaultAnimation(false);

            sprites.SetDeadSprite();
            ReleaseObject();
        }

        private void OnRebirth()
        {
            animator_Base.SetIsPlayDefaultAnimation(true);
            animator_Base.ChangeAnimation(AnimationType.Walk);
            // animatorBase.EnableAnimator();
            sprites.SetRebirthSprite();
            RegisterObject();
        }

        public void SetSlotIndex(int index)
        {
            if (index == -1)
                ReleaseObject();
            else if( index != InGameSlotIndex)
                RegisterObject();
            InGameSlotIndex = index;
        }

        public void SetModelBackImmediate()
        {
            modelTransform.transform.position = SlotManager.Instance.GetStandTransform(InGameSlotIndex).position;
        }

        public void SetHeroData(HeroData newHeroData)
        {
            var stats = GetRef<HeroEntityStats>();
            stats.SetHero(newHeroData);
        }

        [Button]
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
            base.RegisterObject();
            Debug.Log("Register hero",gameObject);
            CombatEntitiesManager.Instance.AppendEntityToListByTag(this, GameTag.Hero);
            CreateAttack();
        }

        public override void ReleaseObject()
        {
            base.ReleaseObject();
            StopExecute();
            Debug.Log("Release hero",gameObject);
            CombatEntitiesManager.Instance.RemoveEntityByTag(this, GameTag.Hero);
            //gameObject.SetActive(false);
        }
    }
}