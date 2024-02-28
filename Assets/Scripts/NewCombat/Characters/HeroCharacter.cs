using Characters;
using CombatSystem;
using Helper;
using NewCombat.AttackFactory;
using NewCombat.ManagerInEntity;
using NewCombat.Slots;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewCombat.Characters
{
    public class HeroCharacter : EntityCharacter
    {
        public CharacterEnumType characterEnumType;
        public int InGameSlotIndex;
        public int UIGameSlotIndex;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private HeroSingleAttackFactory attackFactory;
        [SerializeField] public Transform ModelDrag;
        protected override void Awake()
        {
            base.Awake();
            RegisterObject();
            gameObject.layer = LayerMask.NameToLayer(GameLayerMask.Hero);
        }

        private void Start()
        {
            CreateAttack();
        }

        public void SetSlotIndex(int index)
        {
            if (index == -1)
            {
                //Debug.Log("Hero Active False");
                ReleaseObject();
            }
            else if (InGameSlotIndex == -1)
            {
                RegisterObject();
            }
            InGameSlotIndex = index;

        }
        public virtual void CreateAttack()
        {
            if (attackFactory == null)
            {
                return;
            }
            attackControl.Create(attackFactory);
        }

        public void SetAttackFactory(HeroSingleAttackFactory baseAttack)
        {
            this.attackFactory = baseAttack;
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
            CombatEntitiesManager.Instance.AppendEntityToListByTag(gameObject, GameTag.Hero);

            attackManager.SetAllowExecuteAttackValue(true);
            attackManager.SetTimeCounterValue(true);
        }

        public override void ReleaseObject()
        {
            attackManager.SetAllowExecuteAttackValue(false);
            attackManager.SetTimeCounterValue(false);

            CombatEntitiesManager.Instance.RemoveEntityByTag(gameObject, GameTag.Hero);
            gameObject.SetActive(false);
        }
    }
}