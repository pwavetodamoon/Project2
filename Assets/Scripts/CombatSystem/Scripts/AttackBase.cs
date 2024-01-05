using DG.Tweening;
using UnityEngine;
using System.Collections;

public class AttackBase : MonoBehaviour
{
    public void Init(BaseData data)
    {
        this.data = data;
        IsAttack = false;
    }
    protected BaseData data;
    protected bool IsAttack = false;
    public IEnumerator GoToEnemy(Vector2 enemyPos)
    {
        // di chuyen
        //animator.ChangeState(1);
        yield return transform.DOMove(enemyPos, data.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
    }
    public IEnumerator AttackEnemy()
    {
        // tan cong
        yield return new WaitForSeconds(data.attackTime);
    }
    public IEnumerator GoBackPosition(Vector2 originalPosition)
    {
        // di chuyen ve lai
        yield return transform.DOMove(originalPosition, data.attackTime).SetEase(Ease.OutFlash).WaitForCompletion();
        yield return null;
    }
}

