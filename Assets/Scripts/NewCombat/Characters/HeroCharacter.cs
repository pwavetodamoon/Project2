using Characters;
using CombatSystem;
using Helper;
using NewCombat.AttackFactory;
using NewCombat.HeroDataManager.Data;
using NewCombat.ManagerInEntity;
using NewCombat.Slots;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewCombat.Characters
{
    public class HeroCharacter : EntityCharacter
    {
        public CharacterEnumType characterEnumType;
        public int InGameSlotIndex { get; private set; }
        [SerializeField] private Transform modelTransform;
        [SerializeField] private HeroSingleAttackFactory attackFactory;

        [SerializeField] private bool isDead = false;
        [field: SerializeField] public bool IsDead
        {
            get => isDead;
        }

        protected override void Awake()
        {
            base.Awake();
            gameObject.layer = LayerMask.NameToLayer(GameLayerMask.Hero);
            GetComponent<DamageManager>().OnDie += OnDead;
        }
        private void OnDead()
        {
            isDead = true;
        }
        public void SetSlotIndex(int index)
        {
            if (index == -1)
            {
                ReleaseObject();
            }
            else 
            {
                RegisterObject();
            }
            InGameSlotIndex = index;

        }


        public void SetHeroData(HeroData newHeroData)
        {
            GetComponent<HeroEntityStats>().SetHero(newHeroData);
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

        public void GetModelToOriginalTransform()
        {

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
            gameObject.SetActive(false);
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