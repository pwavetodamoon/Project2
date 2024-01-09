using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
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
        StartCoroutine(TimeCount());
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
        }
    }
    public override IEnumerator TimeCount()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (attacking == false && timeCounter > 0)
                timeCounter -= Time.deltaTime;
        }
    }
    public override void Attack()
    {
        //StartCoroutine(StartAttack());
    }
    GoToCommand command1;
    GoToCommand command2;
    private void Awake()
    {
        command1 = new GoToCommand()
        {
            Time = .5f,
            Target = Vector2.zero,
            Transform = transform
        };
        command2 = command1;
    }
    [Button]
    void Test1()
    {
        var originalPosition = transform.position;
        command1.Target = (Vector2)CombatManager.GetEnemyPosition?.Invoke(0).transform.position;
        command2.Target = originalPosition;
        attackSequence.AddCommand(command1);
        attackSequence.AddCommand(command2);
    }

}