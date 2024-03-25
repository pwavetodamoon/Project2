using CombatSystem.Attack.Utilities;
using DG.Tweening;
using Helper;
using UnityEngine;

public class FireSkill : HeroSkill
{
    public IDamageable damageable;
    private SpriteRenderer spriteRenderer;
    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public override void DealDamage()
    {
        damageable?.TakeDamage(entityStats);

        Debug.Log("Fire Attack");
    }
    public override void IncreaseAttacker()
    {
        var collider = Physics2D.OverlapBox(transform.position, size, 0, GameLayerMask.Get(GameLayerMask.ENEMY));
        if (collider != null)
        {
            attacker = collider.GetComponent<IAttackerCounter>();
            damageable = collider.GetComponent<IDamageable>();
        }
        attacker?.IncreaseAttackerCount(entityStats);
    }
    public override void DecreaseAttacker()
    {
        attacker?.DecreaseAttackerCount(entityStats);
    }
    public override void Destroy()
    {
        spriteRenderer.DOFade(0, 1).OnComplete(() => Destroy(gameObject));
    }
}