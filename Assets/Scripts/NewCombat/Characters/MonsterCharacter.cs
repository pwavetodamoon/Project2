using System.Collections;
using Characters.Monsters;
using CombatSystem;
using NewCombat.HeroAttack;
using NewCombat.MonsterAttack;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.Characters
{
    public class MonsterCharacter : EntityCharacter, IDamageable
    {
        [SerializeField] private Transform AttackedTransform;
        [SerializeField] private float Speed = 2;
        [SerializeField] private HeroCharacter Hero;
        private bool notMoving = false;
        bool isStartAttackBehaviour = false;

        float xRandomNoise = 0;
        float yRandomNoise = 0;
        protected override void Awake()
        {
            base.Awake();
            //GetComponent<BaseNormalAttack>().enabled = false;
            xRandomNoise = Random.Range(-.2f, .2f);
            yRandomNoise = Random.Range(-.2f, .2f);
            attackControl.InitAttackControl(new MonsterNearAttack(this));
            attackControl.enabled = false;
        }
        private void Update()
        {
            Moving();
        }
        public void StopMoving()
        {
            notMoving = true;
        }
        private void Moving()
        {
            if (notMoving || isStartAttackBehaviour) return;
            transform.Translate(Vector3.left * (Time.deltaTime * Speed));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag != GameTag.TriggerEnemy) return;
            MoveToAttackedPos();
        }
        public void MoveToAttackedPos()
        {
            // Check if the monster is already moving, coroutine will be called once
            if (isStartAttackBehaviour) return;

            notMoving = true;
            StartCoroutine(MoveToAttackedPosCoroutine());
            isStartAttackBehaviour = true;
        }
        private IEnumerator MoveToAttackedPosCoroutine()
        {
            //GetComponent<BaseNormalAttack>().enabled = true;
            attackControl.enabled = true;
            while (true)
            {
                if (Hero == null)
                {
                    Hero = CombatManager.Instance.GetHero();
                    yield return new WaitForSeconds(.1f);
                }
                else if (notMoving)
                {
                    var targetPosition = Hero.Slot.GetAttackerPosition();
                    var direction = targetPosition - transform.position;
                    var isOnTarget = Vector3.Distance(transform.position, targetPosition) < 0.1f;
                    // Check if the monster is on the target position
                    // Allow the counter attack
                    if (!isOnTarget)
                    {
                        direction.Normalize();
                        transform.Translate(Speed * Time.deltaTime * direction);
                    }
                    else
                    {
                        allowCounter = true;
                    }
                }
                yield return new WaitForFixedUpdate();
            }
        }


        protected override void UpdateState(bool boolen)
        {
            base.UpdateState(boolen);
            notMoving = !boolen;
        }

        protected override float PlayHurtAnimation()
        {
            var time = Animator.GetAnimationLength(Monster_Animator.Hurt_State);
            Animator.ChangeAnimation(Monster_Animator.Hurt_State);
            return time;
        }
        public Transform GetAttackedPos() => AttackedTransform;

        protected override void RegisterObject()
        {
            CombatManager.Instance.AddMonster(this);
        }

        protected override void RelaseObject()
        {
            CombatManager.Instance.RemoveMonster(this);
        }
    }

    public interface IDamageable
    {
        void TakeDamage(float damage);
    }
}
