using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class PlayerCharacters : CharactersBase
{
    //public static PlayerCharacters Instance;
    public bool Test = false;
    public float timeAttack = 0;
    private void Start()
    {
        InitCommandList();
        health = GetComponent<HealthBase>();
        health.Setup(data);
        //data = Game_DataBase.Instance.GetPlayerData(ID);

        type = data.AttackType;
        timeAttack = GetComponentInChildren<IGetAnimationLength>().GetAnimationLength(Human_Animator.Slash_State);
        ChangeComponent();
        timeCounter = data.timeCoolDown;
    }
    [SerializeField] ActionSequence attackSequence;
    private void Update()
    {
        if (Test == true)
        {
            return;
        }

        if (timeCounter <= 0 && attacking == false)
        {
            attacking = true;
            Attack();
        }
        else
        {
            timeCounter -= Time.deltaTime;
        }
    }
    public override void Attack()
    {
        var enemyPos = (Vector2)CombatManager.GetEnemyPosition?.Invoke(0).transform.position;

        var originalPos = transform.position;

        AttackCommands[0] = new ActionCommand(MoveToEnemyCoroutine(enemyPos));
        AttackCommands[2] = new ActionCommand(MoveToEnemyCoroutine(originalPos));

        attackSequence.AddListCommands(AttackCommands);
    }


    protected override void InitCommandList()
    {
        ICommand moveCommand = new ActionCommand(MoveToEnemyCoroutine(Vector2.zero));
        ICommand attackCommand = new ActionCommand(null, AttackEnemy, timeAttack);
        ICommand moveCommand2 = new ActionCommand(MoveToEnemyCoroutine(Vector2.zero));
        ICommand attackCommand2 = new ActionCommand(null, ResetState, 0);

        AttackCommands.Add(moveCommand);
        AttackCommands.Add(attackCommand);
        AttackCommands.Add(moveCommand2);
        AttackCommands.Add(attackCommand2);
    }

    void AttackEnemy()
    {
        animator.ChangeAnimation(Human_Animator.Slash_State);
    }
    void ResetState()
    {
        animator.ChangeAnimation(Human_Animator.Idle_State);
        attacking = false;
        timeCounter = data.timeCoolDown;
    }
    //protected IEnumerator MoveToEnemyCoroutine(Vector2 enemyPos,float time = 1)
    //{
    //    animator.ChangeAnimation(Human_Animator.Walk_State);
    //    yield return transform.DOMove(enemyPos, 1).WaitForCompletion();
    //}

}
