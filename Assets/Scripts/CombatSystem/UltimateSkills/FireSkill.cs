using CombatSystem.Attack.Utilities;
using Helper;
using UnityEngine;

public class FireSkill : HeroSkill
{
    public override void DealDamage()
    {
        // deal damage to one enemy
        var collider = Physics2D.OverlapBox(transform.transform.position, size, 0, GameLayerMask.Get(GameLayerMask.ENEMY));
        if (collider != null)
        {
            collider.GetComponent<IDamageable>().TakeDamage(damage);
        }
        Debug.Log("Fire Attack");
    }
}