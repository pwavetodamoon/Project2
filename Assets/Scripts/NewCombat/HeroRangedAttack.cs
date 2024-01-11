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
        yield return null;
        SetAttackToDeactive();
    }
}
