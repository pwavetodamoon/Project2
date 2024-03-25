using UnityEngine;

[CreateAssetMenu(fileName = "UlSkill_", menuName = "UltimateSkill")]
public class UltimateSkill : ScriptableObject
{
    public GameObject skillPrefab;
    public float skillCooldown;
    public float skillDamage;
    public void Execute()
    {
        var go = Instantiate(skillPrefab);
        var skill = go.GetComponent<FireAttack>();
        skill.damage = skillDamage;
        skill.animator.Play("Effect");
    }
}
