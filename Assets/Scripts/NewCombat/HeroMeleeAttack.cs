using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMeleeAttack : HeroNormalAttack
{
    public float timeCounter;
    public float maxTime = 0.5f;
    public int attackCount;
    public int maxAttackCount = 3;
    private void Update()
    {
        if (timeCounter > 0 && isActive == false)
            timeCounter -= Time.deltaTime;
        else
        {
            timeCounter = 0;
        }
    }
    protected override void OnDrawGizmos()
    {
        if (gizmosTransform == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawCube(gizmosTransform.position, size);
    }
    protected override void CheckCollider()
    {
        if (gizmosTransform == null) return;
        var colliders = Physics2D.OverlapBoxAll(gizmosTransform.position, size, angle);
        foreach (var collider in colliders)
        {
            Debug.Log(collider.name);
            if (collider.TryGetComponent(out MonsterCharacter monster))
            {
                Debug.Log("Monster is hit");
            }
        }
    }
    protected override IEnumerator StartBehavior(HeroCharacter hero)
    {
        MonsterCharacter monster = CombatManager.Instance.GetMonster();
        if (monster == null)
        {
            Debug.Log("Target is null");
            yield break;
        }
        yield return MoveToTransform(hero.transform, monster.GetAttackerPosition());
        yield return AttackBetween();
        yield return MoveToTransform(hero.transform, hero.Slot.GetCharacterPosition());
        isActive = false;
    }
    private IEnumerator AttackBetween()
    {
        this.animator.ChangeAnimation(Human_Animator.Slash_State);
        var time = animator.GetAnimationLength(Human_Animator.Slash_State);
        Debug.Log("AttackBetween: " + time);
        yield return new WaitForSeconds(time);
        CheckCollider();
    }
    private IEnumerator MoveToTransform(Transform Character, Vector3 TargetPosition)
    {
        animator.ChangeAnimation(Human_Animator.Walk_State);
        while (true)
        {
            var direction = TargetPosition - Character.position;
            direction.Normalize();
            Character.Translate(speed * Time.deltaTime * direction);
            if (Vector3.Distance(Character.position, TargetPosition) < 0.1f)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.ChangeAnimation(Human_Animator.Idle_State);
        
    }
}
