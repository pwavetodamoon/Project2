using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Sirenix.OdinInspector;

public class HeroNormalAttack : MonoBehaviour, IHeroAttack
{
    public bool isActive = false;
    public float speed = 2;
    Animator_Base animator;
    public Transform gizmosTransform;
    public Vector2 gizmosPosition;
    public Vector2 size = Vector3.one;
    public float angle = 0;
    // Draw the gizmo
    void OnDrawGizmos()
    {
        if (gizmosTransform == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawCube(gizmosTransform.position, size);
    }
    // Check the collider in the gizmo
    [Button]
    void CheckCollider()
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
    // Execute the attack, this is interface method
    public void ExecuteAttack(Animator_Base animator)
    {
        if (isActive) return;
        isActive = true;
        this.animator = animator;
        StartCoroutine(StartBehavior());
    }
    // Start the attack behavior with a coroutine
    private IEnumerator StartBehavior()
    {
        HeroCharacter character = GetComponentInParent<HeroCharacter>();
        MonsterCharacter monster = CombatManager.Instance.GetMonster();
        if (monster == null)
        {
            Debug.Log("Target is null");
            yield break;
        }
        yield return MoveToTransform(character.transform, monster.GetAttackerPosition());
        yield return AttackBetween();
        yield return MoveToTransform(character.transform, character.Slot.GetCharacterPosition());
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
                Debug.Log("MoveToTransform: " + Vector3.Distance(Character.position, TargetPosition));
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.ChangeAnimation(Human_Animator.Idle_State);
        isActive = false;
    }
}
public interface IHeroAttack
{
    void ExecuteAttack(Animator_Base animator);
}
