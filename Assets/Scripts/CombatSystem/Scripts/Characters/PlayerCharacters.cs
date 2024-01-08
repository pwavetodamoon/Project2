using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

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
    [SerializeField] AttackSequence attackSequence;
    private void Update()
    {
        if (Test == true)
        {
            return;
        }

        if (timeCounter <= 0 && attacking == false)
        {
            attacking = true;
            
            //attacking = true;
            //CombatManager.AddPlayerAction(Attack);
            //var playerdata = data;
            //timeCounter = playerdata.timeCoolDown + playerdata.animationTime + playerdata.attackTime;
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
        StartCoroutine(StartAttack());
    }

    [Button]
    private IEnumerator StartAttack()
    {
        var Enemy = CombatManager.GetEnemyPosition?.Invoke(0);
        Vector2 originalPosition = transform.position;
        var enemyPos = Enemy == null ? transform.position : Enemy.transform.position;

        //GetComponentInChildren<Human_Animator>().ChangeAnimation(1);
        yield return normalAttack.StartCoroutine(normalAttack.GoToEnemy(enemyPos));
        //GetComponentInChildren<Human_Animator>().ChangeAnimation(2);
        yield return normalAttack.StartCoroutine(normalAttack.AttackEnemy());
        //GetComponentInChildren<Human_Animator>().ChangeAnimation(1);

        Enemy.TakeDamage(data.damage);
        yield return normalAttack.StartCoroutine(normalAttack.GoBackPosition(originalPosition));
        attacking = false;
    }
    [Button]
    void Test1()
    {
        var originalPosition = transform.position;
        attackSequence.AddCommand(new GoToCommand()
        {
            time = .5f,
            target = (Vector2)CombatManager.GetEnemyPosition?.Invoke(0).transform.position,
            transform = transform
        });
        attackSequence.AddCommand(new GoToCommand()
        {
            time = .5f,
            target = originalPosition,
            transform = transform
        });
    }
}