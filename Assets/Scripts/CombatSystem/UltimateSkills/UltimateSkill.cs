using System.Collections;
using System.Collections.Generic;
using CombatSystem;
using CombatSystem.Entity;
using Helper;
using Sirenix.OdinInspector;
using SlotHero.Grid;
using UnityEngine;

public class UltimateSkill : UltimateSkillBase
{
    public bool useUnlimited = false;
    public int fireBallCount = 5;
    public override void Execute(CustomGrid CustomGrid)
    {
        if (!CanUseSkill) return;
        for (int i = 0; i < fireBallCount; i++)
        {

            var randomTime = Random.Range(1, .1f);
            StartCoroutine(Execute(CustomGrid.GetRandomPosition(), randomTime));
        }
        if (useUnlimited == false)
            timer = skillCooldown;
    }
    public IEnumerator Execute(Vector3 SpawnPos, float delay)
    {
        yield return new WaitForSeconds(delay);
        CreateFireBall(SpawnPos);
    }


    private void CreateFireBall(Vector3 newPosition)
    {
        var go = Instantiate(skillPrefab, newPosition + CreateRandomPos(), Quaternion.identity);
        var skill = go.GetComponent<HeroSkill>();
        skill.Play();
    }
    private Vector3 CreateRandomPos()
    {
        float value = .5f;
        return new Vector3(Random.Range(-value, value), Random.Range(-value, value));
    }
}
public abstract class UltimateSkillBase : MonoBehaviour
{
    public GameObject skillPrefab;
    public float skillCooldown;
    public float timer;
    public bool CanUseSkill => timer <= 0;
    public abstract void Execute(CustomGrid CustomGrid);

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
