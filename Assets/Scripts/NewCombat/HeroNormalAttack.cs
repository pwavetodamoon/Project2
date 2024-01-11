using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Sirenix.OdinInspector;

public class HeroNormalAttack : MonoBehaviour, IHeroAttack
{
    public bool isActive = false;
    public float speed = 2;
    protected Animator_Base animator;
    public Transform gizmosTransform;
    public Vector2 gizmosPosition;
    public Vector2 size = Vector3.one;
    public float angle = 0;
    public HeroCharacter hero;
    protected virtual HeroCharacter GetCharacter()
    {
        return GetComponentInParent<HeroCharacter>();
    }
    // Draw the gizmo
    protected virtual void OnDrawGizmos()
    {
        
    }
    // Check the collider in the gizmo
    [Button]
    protected virtual void CheckCollider()
    {
        
    }
    // Execute the attack, this is interface method
    public void ExecuteAttack(Animator_Base animator)
    {
        if (isActive) return;
        isActive = true;
        this.animator = animator;
        StartCoroutine(StartBehavior(GetCharacter()));
    }

    // Start the attack behavior with a coroutine
    protected virtual IEnumerator StartBehavior(HeroCharacter hero)
    {
        yield return null;
        SetAttackToDeactive();
    }
    public void SetAttackToDeactive()
    {
        isActive = false;
    }
}
public interface IHeroAttack
{
    void ExecuteAttack(Animator_Base animator);
}
