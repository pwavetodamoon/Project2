using System.Collections;
using Characters.Monsters;
using CombatSystem;
using NewCombat.HeroAttack;
using Sirenix.OdinInspector;
using UnityEngine;
namespace NewCombat.Characters
{
    public class MonsterCharacter : EntityCharacter, IDamageable
    {
        [SerializeField] private Transform AttackedTransform;

        public float Speed = 2;
        public float attackRange = .5f;
        public HeroCharacter Hero;
        public bool notMoving = false;
        public bool IsTrackHero = false;
        bool isStartAttackBehaviour = false;

        float xRandomNoise = 0;
        float yRandomNoise = 0;
        protected override void Awake()
        {
            base.Awake();
            GetComponent<BaseNormalAttack>().enabled = false;
            xRandomNoise = Random.Range(-.2f, .2f);
            yRandomNoise = Random.Range(-.2f, .2f);

            CombatManager.Instance.AddMonster(this);
        }
        void Update()
        {
            Moving();
        }
        private void Moving()
        {
            if (notMoving || isStartAttackBehaviour) return;
            transform.Translate(Vector3.left * (Time.deltaTime * Speed));
        }
        [Button]
        public void StartCoroutine()
        {
            if (isStartAttackBehaviour == false)
            {
                notMoving = true;
                StartCoroutine(MoveToAttkedPos());
                isStartAttackBehaviour = true;
            }
        }
        [HideInEditorMode]
        public IEnumerator MoveToAttkedPos()
        {
            GetComponent<BaseNormalAttack>().enabled = true;
            //if (Hero == null) yield break;
            while (true)
            {
                if (Hero != null && notMoving)
                {
                    var TargetPosition = Hero.Slot.GetAttackerPosition();
                    var direction = TargetPosition - transform.position;
                    var isOnTarget = false;

                    if (Vector3.Distance(transform.position, TargetPosition) < 0.1f)
                    {
                        isOnTarget = true;
                        allowCounter = true;
                    }
                    else
                    {
                        isOnTarget = false;
                        allowCounter = false;
                    }
                    if (isOnTarget == false)
                    {
                        direction.Normalize();
                        transform.Translate(Speed * Time.deltaTime * direction);
                    }
                }
                else if(Hero == null)
                {
                    Hero = CombatManager.Instance.GetHero();
                    yield return new WaitForSeconds(.1f);
                }
                yield return new WaitForFixedUpdate();
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger Enter");
            if (collision.tag != GameTag.TriggerEnemy) return;
            Debug.Log("Trigger Enemy");
            StartCoroutine();
        }
        protected override void ResetState(bool boolen)
        {
            base.ResetState(boolen);
            notMoving = !boolen;
        }
        public Transform GetAttackerPosition()
        {
            if (AttackedTransform == null)
            {
                return transform;
            }
            return AttackedTransform;
        }
        protected override float PlayHurtAnimation()
        {
            var time = Animator.GetAnimationLength(Monster_Animator.Hurt_State);
            Animator.ChangeAnimation(Monster_Animator.Hurt_State);
            return time;
        }
    }
    public interface IDamageable
    {
        void TakeDamage(float damage);
    }
}
