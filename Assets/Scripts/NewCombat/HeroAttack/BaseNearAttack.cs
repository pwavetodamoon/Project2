using NewCombat.HeroAttack;
using UnityEngine;

public class BaseNearAttack : BaseNormalAttack
{
    public Transform AttackTransform;
    public Vector2 gizmosPosition;
    public Vector2 size;
    public string Tag = "Enemy";

    protected float Angle = 0;
    public void OnDrawGizmos()
    {
        if (AttackTransform == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawCube(AttackTransform.position, size);
    }



    protected override void CauseDamage(string Tag)
    {
        if (AttackTransform == null)
        {
            Debug.LogWarning("AttackTransform is null", gameObject);
            return;
        }
        CombatCollider.CheckOverlapBox(Tag, AttackTransform.position, size, Angle);
    }
}