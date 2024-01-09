using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
public class PlayerCharacters : CharactersBase
{
    //public static PlayerCharacters Instance;
    public bool Test = false;
    private void Start()
    {
        health = GetComponent<HealthBase>();
        health.Setup(data);
        //data = Game_DataBase.Instance.GetPlayerData(ID);

        type = data.AttackType;

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
        ICommand command1 = CreateGoToCommand(enemyPos, 1);
        ICommand command2 = CreateGoToCommand(transform.position, 1, () => {
            attacking = false;
            timeCounter = data.timeCoolDown; 
        });

        attackSequence.AddCommand(command1);
        attackSequence.AddCommand(command2);
    }
    private ICommand CreateGoToCommand(Vector2 target, float time, Action callback = null)
    {
        ICommandBehavior goToCommand = new GoToCommand()
        {
            Transform = transform,
            Target = target,
            Time = time
        };
        return new ActionCommand(goToCommand, callback, 0);
    }

}