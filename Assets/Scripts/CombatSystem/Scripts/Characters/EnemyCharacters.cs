using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class EnemyCharacters : CharactersBase
{
    private EnemyMoving moving;
    public ActionSequence attackSequence;
    private void Start()
    {
        health = GetComponent<HealthBase>();
        moving = GetComponent<EnemyMoving>();
        health.Setup(data);
        moving.Setup(1);
        ChangeComponent();
        InitCommandList();
    }

    public CharactersBase enemy;

    public void StartAttack()
    {
        moving.isMoving = false;
    }
    public override void Attack()
    {
        Debug.Log("Attack");
        attackSequence.AddListCommands(AttackCommands);
    }

    public bool Test = false;
    public bool allowAttack;
    private void Update()
    {
        if (Test == true)
        {
            return;
        }
        if(allowAttack == false)
        {
            return;
        }
        if (timeCounter <= 0 && attacking == false)
        {
            attacking = true;
            allowAttack = false;
            Attack();
        }
        else
        {
            timeCounter -= Time.deltaTime;
        }
    }
    void ResetState()
    {
        animator.ChangeAnimation(Human_Animator.Walk_State);
        attacking = false;
        timeCounter = data.timeCoolDown;
    }
    protected override void InitCommandList()
    {
        float timeAttack = animator.GetAnimationLength(Monster_Animator.Attack_State);
        //AttackCommands.Add(new ActionCommand(MoveToEnemyCoroutine(Vector2.zero), null, 0));
        AttackCommands.Add(new ActionCommand(null, AttackEnemy, timeAttack + .1f));
        AttackCommands.Add(new ActionCommand(null, ResetState, 0));
        AttackCommands.Add(new ActionCommand(null, CheckEnemy, 0));
    }
    void AttackEnemy()
    {
        animator.ChangeAnimation(Monster_Animator.Attack_State);
        if (enemy == null)
        {
            return;
        }

    }
    [Button]
    public void CheckEnemy()
    {
        var newEnemy = CombatManager.GetEnemyPosition?.Invoke(1);
        if(newEnemy == null)
        {
            return;
        }
        if (enemy == null)
        {
            enemy = newEnemy;
            var enemyPos = (Vector2)newEnemy.transform.position + new Vector2(2, 0);
            // di chuyen toi muc tieu va cho phep tan cong
            attackSequence.AddCommand(new ActionCommand(MoveToEnemyCoroutine(enemyPos), () => { allowAttack = true; }, 0));
        }
        else
        {
            allowAttack = true;
        }
        Debug.Log("Check enemy");
    }
}