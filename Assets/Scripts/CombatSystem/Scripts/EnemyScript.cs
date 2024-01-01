using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static EnemyScript Instance;

    [SerializeField] Enemy enemyData;
    BaseData baseData;
    [SerializeField] Transform pos;
    [SerializeField] private float timeCounter;
    public float speed;
    public string ID;
    [SerializeField] AttackTypeEnum type;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        enemyData = Game_DataBase.Instance.GetEnemyData(ID);
        ChangeComponent();
        type = enemyData.AttackType;
        pos = enemyData.Base;
        StartCoroutine(TimeCount());

    }
    private void Update()
    {
        if (timeCounter == 0)
        {
            //Attack();
            timeCounter = enemyData.timeCoolDown;
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
