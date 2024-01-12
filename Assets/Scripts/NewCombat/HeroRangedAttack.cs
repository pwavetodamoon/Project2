using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRangedAttack : HeroNormalAttack
{
    public GameObject projectilePrefab;

    protected override IEnumerator StartBehavior(HeroCharacter hero)
    {
        MonsterCharacter monster = CombatManager.Instance.GetMonster();
        if (monster == null)
        {
            Debug.Log("Target is null");
            yield break;
        }
        yield return Fire();
        isActive = false;
    }
    IEnumerator Fire()
    {
        animator.ChangeAnimation(Human_Animator.Slash_State);
        var time = animator.GetAnimationLength(Human_Animator.Slash_State);
        yield return new WaitForSeconds(time/2);
        SpawnProjectile();
        
    }
    protected Transform SpawnProjectile()
    {
        var monster = CombatManager.Instance.GetMonster();
        if (monster == null) { return null; }
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.transform.position = gizmosPosition;
        projectile.GetComponent<Projectile>().SetTarget(monster.transform);
        return projectile.transform;
    }
}
