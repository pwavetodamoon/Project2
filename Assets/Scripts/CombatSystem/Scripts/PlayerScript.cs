using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    [SerializeField] public Player playerData;
    [SerializeField] Transform pos;
    [SerializeField] private float timeCounter;
  
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

        //playerData = Game_DataBase.Instance.GetPlayerData(ID);
        
        type = playerData.AttackType;

        ChangeComponent();
        timeCounter = playerData.timeCoolDown;
        StartCoroutine(TimeCount());
    }
    private void Update()
    {
        playerData.Slot = GetComponentInParent<Transform>();
        pos = playerData.Slot;

        //Uu tien danh don manh truoc
        if (timeCounter <= 0)
        {
            
            Attack();
            timeCounter = playerData.timeCoolDown + playerData.animationTime + playerData.attackTime;
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
        return;

    }
    public AttackBase normalAttack;
    [Button]
    void Attack()
    {
        Debug.Log("Attack");
        GetComponent<IAttack>().Attack(playerData);
       
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
