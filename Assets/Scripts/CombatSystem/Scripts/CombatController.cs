using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game_DataBase;

public class CombatController : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject Base;
    [SerializeField] Transform shootingPos;
    [SerializeField] float attackSpeed;
    [SerializeField] float animationTime;
    [SerializeField] float timeCoolDown;
    [SerializeField] float timeCounter;
    [SerializeField] string ID;
    [SerializeField] bool isAttacking;
    AttackType attacktype;
    WeaponType weaponType;

    [Button]
    IEnumerator TanCongTamGan(float AttackSpeed)
    {
        Debug.Log("Dang tan cong");
        // Di chuyen den vi tri tan cong
        yield return transform.DOMove(Enemy.transform.position, attackSpeed).SetEase(Ease.OutFlash).WaitForCompletion();
        // chay animation
        yield return new WaitForSeconds(AttackSpeed);
        Debug.Log("Tan cog xong");
        // Quay lai vi tri ban dau
        yield return transform.DOMove(Base.transform.position, attackSpeed).SetEase(Ease.OutFlash).WaitForCompletion();

        StartCoroutine(TimeCount());
        isAttacking = false;
    }
    IEnumerator TanCongTamXa(float AttackSpeed)
    {

        yield return Instantiate(weaponPrefab, shootingPos.position, Quaternion.identity);
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
    private void Start()
    {

        // Lấy Data theo ID
        playerData = Game_DataBase.Instance.GetPlayerData(ID);
        attacktype = playerData.playerAttackType;
        animationTime = playerData.animationSpeed;
        attackSpeed = playerData.attackSpeed;
        timeCoolDown = playerData.coolDown;
        weaponType = playerData.playerWeaponType;
        weaponPrefab = playerData.weaponPrefab;



        //Set value
        timeCounter = timeCoolDown;
        StartCoroutine(TimeCount());

    }
    private void Update()
    {
        //Uu tien danh don manh truoc
        if (timeCounter == 0 && !isAttacking)
        {
            Debug.Log("Trung");
            StopAllCoroutines();
            if (attacktype == AttackType.Near)
            {
                StartCoroutine(TanCongTamGan(animationTime));
            }
            if (attacktype == AttackType.Far)
            {
                StartCoroutine(TanCongTamXa(animationTime));
            }
            timeCounter = timeCoolDown;
        }
    }
}

