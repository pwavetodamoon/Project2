using System.Collections.Generic;
using CombatSystem;
using CombatSystem.Entity;
using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "UlSkill_", menuName = "UltimateSkill")]
public class UltimateSkill : UltimateSkillBase
{
    public int fireBallCount = 3;
    public override void Execute(Transform locatedPos)
    {
        if (!CanUseSkill) return;
        if (locatedPos == null) return;
        var listEnemy = Get3MonsterRandom();
        for (int i = 0; i < listEnemy.Count; i++)
        {
            if (listEnemy.Count == 0) return;
            var index = Random.Range(0, listEnemy.Count);
            var enemy = listEnemy[index];
            if (enemy == null) return;
            CreateFireBall(enemy);
        }
        timer = skillCooldown;
    }
    private List<EntityCharacter> Get3MonsterRandom()
    {
        var enemies = new List<EntityCharacter>(CombatEntitiesManager.Instance.GetEnemies());
        var list = new List<EntityCharacter>();

        while (list.Count < fireBallCount && enemies.Count > 0)
        {
            var index = Random.Range(0, enemies.Count);
            var enemy = enemies[index];
            if (enemy == null) continue;
            list.Add(enemy);
            enemies.RemoveAt(index);
        }
        return list;
    }
    private void CreateFireBall(EntityCharacter enemy)
    {
        var go = Instantiate(skillPrefab, enemy.transform.position, Quaternion.identity);
        var skill = go.GetComponent<HeroSkill>();
        skill.animator.Play("Effect");
    }
}
public abstract class UltimateSkillBase : MonoBehaviour
{
    public GameObject skillPrefab;
    public float skillCooldown;
    public float timer;
    public bool CanUseSkill => timer <= 0;
    public abstract void Execute(Transform locatedPos);

    private void Update()
    {
        if (timer < 0)
        {
            timer = 0;
            return;
        }
        timer -= Time.deltaTime;
    }
}
