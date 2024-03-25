using CombatSystem;
using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "UlSkill_", menuName = "UltimateSkill")]
public class UltimateSkill : ScriptableObject
{
    public GameObject skillPrefab;
    public float skillCooldown;
    public float skillDamage;
    public void Execute(Transform locatedPos)
    {
        if (locatedPos == null) return;
        var enemy = CombatEntitiesManager.Instance.GetEntityTransformByTag(locatedPos, GameTag.Enemy);
        if (enemy == null) return;


        var go = Instantiate(skillPrefab, enemy.transform.position, Quaternion.identity);
        var skill = go.GetComponent<FireAttack>();
        skill.damage = skillDamage;
        skill.animator.Play("Effect");
    }
}
