using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Enemy EnemyData;
    [SerializeField] Transform pos;
    [SerializeField] private float timeCounter;
    [SerializeField] float speed;
    [SerializeField] string ID; 
    [SerializeField] AttackTypeEnum type;

    private void Start()
    {
        EnemyData = Game_DataBase.Instance.GetEnemyData(ID);
        ChangeComponent();
        type = EnemyData.AttackType;
        StartCoroutine(TimeCount());
    }
    private void Update()
    {
        if (timeCounter == 0)
        {
            Attack();
            timeCounter = EnemyData.timeCoolDown;
        }
    }
    void Attack()
    {
        GetComponent<IAttack>().Attack();
    }
    public void ChangeComponent()
    {
        
        if (GetComponent<AttackBase>() != null)
        {
            DestroyImmediate(GetComponent<AttackBase>());
        }
        if (type == AttackTypeEnum.Near)
        {
            transform.AddComponent<ShortRange>();
        }
        if (type == AttackTypeEnum.Far)
        {
            transform.AddComponent<LongRange>();

        }
        GetComponent<AttackBase>().enemyData = Game_DataBase.Instance.GetEnemyData(ID);

    }
    IEnumerator TimeCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeCounter--;
            yield return null;
        }
    }

}
