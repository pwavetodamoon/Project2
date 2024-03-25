using System.Collections.Generic;
using CombatSystem;
using CombatSystem.Entity;
using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "UlSkill_", menuName = "UltimateSkill")]
public class UltimateSkill : ScriptableObject
{
    public GameObject skillPrefab;
    public float skillCooldown;
    public float skillDamage;
    public int fireBallCount = 3;
    public void Execute(Transform locatedPos)
    {
        if (locatedPos == null) return;

        var listEnemy = Get3MonsterRandom();
        for (int i = 0; i < fireBallCount; i++)
        {
            if (listEnemy.Count == 0) return;
            var index = Random.Range(0, listEnemy.Count);
            var enemy = listEnemy[index];
            if (enemy == null) return;
            CreateFillBall(enemy);
        }
    }
    private List<EntityCharacter> Get3MonsterRandom()
    {
        var list = new List<EntityCharacter>();
        var enemies = new List<EntityCharacter>(CombatEntitiesManager.Instance.GetEnemies());
        while (list.Count < 3 && enemies.Count > 0)
        {
            var index = Random.Range(0, enemies.Count);
            var enemy = enemies[index];
            if (enemy == null) continue;
            list.Add(enemy);
            enemies.RemoveAt(index);
        }
        return list;
    }
    private void CreateFillBall(EntityCharacter enemy)
    {
        var go = Instantiate(skillPrefab, enemy.transform.position, Quaternion.identity);
        var skill = go.GetComponent<HeroSkill>();
        skill.damage = skillDamage;
        skill.animator.Play("Effect");
    }
}
public abstract class UltimateSkillBase : ScriptableObject
{
    public GameObject skillPrefab;
    public abstract void Execute(Transform locatedPos);
}
