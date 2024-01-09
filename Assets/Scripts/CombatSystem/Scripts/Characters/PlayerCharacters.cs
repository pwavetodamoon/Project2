using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using DG.Tweening;
using System.Data;
public class PlayerCharacters : CharactersBase
{
    //public static PlayerCharacters Instance;
    public bool Test = false;
    public float timeAttack = 0;
    private void Start()
    {
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
            Test1();
        }
        else
        {
            timeCounter -= Time.deltaTime;
        }
    }
    public override void Attack()
    {
        //StartCoroutine(StartAttack());
    }
    [Button]
    void Test1()
    {
        var enemyPos = (Vector2)CombatManager.GetEnemyPosition?.Invoke(0).transform.position;
        var originalPos = transform.position;
        // di toi dich
        var animator = GetComponentInChildren<Animator_Base>();

        ActionCommand goToEnemy = new ActionCommand();
        goToEnemy.CommandBehavior = new GoToCommand()
        {
            Transform = transform,
            Target = enemyPos,
            Time = 1
        };
        ActionCommand playAnimation = new ActionCommand(null,null, timeAttack + .1f);
        playAnimation.EndCallbackMethod = () =>
        {
            animator.ChangeAnimation(Human_Animator.Slash_State);
        };



        var command4 = new ActionCommand();
        command4.EndCallbackMethod = () =>
        {
            attacking = false;
            timeCounter = data.timeCoolDown;
            animator.ChangeAnimation(Human_Animator.Idle_State);
        };
        attackSequence.AddCommand(goToEnemy);
        attackSequence.AddCommand(playAnimation);
        attackSequence.AddCommand(goBack);
        attackSequence.AddCommand(command4);
    }


    private ActionCommand CreateGoToCommand(Vector2 target, float time, Action callback = null)
    {
        // Action callback is call when Commandbehavior bellow is done
        ICommandBehavior goToCommand = new GoToCommand()
        {
            Transform = transform,
            Target = target,
            Time = time
        };
        return new ActionCommand(goToCommand, callback, 0);
    }

}