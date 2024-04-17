using CombatSystem.Attack.Utilities;
using CombatSystem.Entity;
using DG.Tweening;
using Helper;
using UnityEngine;
public class ElementSkill : HeroSkill
{
    public IDamageable damageable;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void DealDamage()
    {
        damageable?.TakeDamage(entityStats);
        // spriteRenderer.DOFade(0, 1);

        Debug.Log("Fire Attack");
    }

    public override void IncreaseAttacker()
    {
        var collider = Physics2D.OverlapBox(transform.position, size, 0, GameLayerMask.Get(GameLayerMask.ENEMY));
        if (collider != null)
        {
            var entity = collider.GetComponent<EntityCharacter>();
            attacker = entity.GetRef<IAttackerCounter>();
            damageable = entity.GetRef<IDamageable>();
        }
        attacker?.IncreaseAttackerCount(entityStats);
    }
    public override void DecreaseAttacker()
    {
        attacker?.DecreaseAttackerCount(entityStats);
    }

    public override void Destroy()
    {
        transform.DOKill();
        Destroy(gameObject);
    }
}