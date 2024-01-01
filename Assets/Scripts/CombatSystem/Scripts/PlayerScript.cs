using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    [SerializeField] Player playerData;
    [SerializeField] Transform pos;
    [SerializeField] private float timeCounter;
    [SerializeField] private bool isAttacking;
    [SerializeField] public string ID;
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
        playerData = Game_DataBase.Instance.GetPlayerData(ID);
        pos = playerData.Base;
        type = playerData.AttackType;
        ChangeComponent();
        timeCounter = playerData.timeCoolDown;
        StartCoroutine(TimeCount());
    }
    private void Update()
    {
        
        //Uu tien danh don manh truoc
        if (timeCounter <= 0)
        {
           
            Attack();
            timeCounter =  playerData.timeCoolDown + playerData.animationTime + playerData.attackTime;
        }
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
        GetComponent<AttackBase>().enemyData = Game_DataBase.Instance.GetEnemyData("E01");
        GetComponent<AttackBase>().playerData = Game_DataBase.Instance.GetPlayerData(ID);
    }
    public AttackBase normalAttack;
    [Button]
    void Attack()
    {
        GetComponent<IAttack>().Attack();
        playerData.weaponPrefab.SetActive(true);
    }
    IEnumerator TimeCount()
    {
        while (true)
        {
            //yield return new WaitForSeconds(1);
            timeCounter -= Time.deltaTime;
            yield return null;
        }
    }

}
