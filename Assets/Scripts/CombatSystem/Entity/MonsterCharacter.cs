using CombatSystem.Attack.Factory;
using CombatSystem.Entity.Utilities;
using CombatSystem.Helper;
using Core.Reward;
using Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CombatSystem.Entity
{
    [RequireComponent(typeof(DamageSlashEffect))]
    public class MonsterCharacter : EntityCharacter
    {

        public MonsterNearSingleAttackFactory monsterSingleAttackFactory;
        protected EntityStateManager EntityStateManager;
        private float xRandomNoise;
        private float yRandomNoise;
        protected override void Awake()
        {
            base.Awake();
            EntityStateManager = GetComponent<EntityStateManager>();
            EntityStateManager.OnDie += EntityStateManagerOnDie;

        }

        private void Start()
        {
            HealthBarManager.Instance.GetHealthBars(this);

            attackControl.Create(monsterSingleAttackFactory);
        }

        private void OnDisable()
        {
            //Debug.Log("OnDisable",gameObject);
            EntityStateManager.OnDie -= EntityStateManagerOnDie;
        }

        private void EntityStateManagerOnDie()
        {
            GetComponent<RewardSignal>().SendSignal();

        }

        private void CreateNoise()
        {
            xRandomNoise = Random.Range(-3f, 3f);
            yRandomNoise = Random.Range(-3f, .3f);
        }


        public override void RegisterObject()
        {
            base.RegisterObject();
            CombatEntitiesManager.Instance.AppendEntityToListByTag(this, GameTag.Enemy);
            CreateNoise();
        }
        public override void ReleaseObject()
        {
            base.ReleaseObject();
            attackControl.StopExecute();
            // Debug.Log("On Get out : "+transform.name,gameObject);
            CombatEntitiesManager.Instance.RemoveEntityByTag(this, GameTag.Enemy);
            //Destroy(gameObject);
        }

        public void KillMonster()
        {
            Destroy(gameObject);
        }
    }

}