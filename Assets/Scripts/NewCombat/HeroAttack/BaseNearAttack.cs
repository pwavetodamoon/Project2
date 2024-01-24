using NewCombat.Characters;
using NewCombat.HeroAttack;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseNearAttack : BaseNormalAttack
{
    [Title("Near Attack Settings")]
    public Vector2 gizmosPosition;
    public Vector2 size;
    protected float Angle = 0;

    protected BaseNearAttack(EntityCharacter newEntityCharacter, Transform attackTransform = null) : base(newEntityCharacter, attackTransform)
    {
    }

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
            Debug.LogWarning("AttackTransform is null");
            return;
        }
        CombatCollider.CheckOverlapBox(Tag, AttackTransform.position, size, Angle);
    }
}