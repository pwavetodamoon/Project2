using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UltimateSkillAction : MonoBehaviour
{
    public UnityEvent IncreaseAttackerAction;
    public UnityEvent DecreaseAttackerAction;
    public UnityEvent DealDamageAction;
    public UnityEvent DestroyAction;
    public HeroSkill heroSkill;
    private void Awake()
    {
        heroSkill = GetComponentInParent<HeroSkill>();
        IncreaseAttackerAction.AddListener(heroSkill.IncreaseAttacker);
        DecreaseAttackerAction.AddListener(heroSkill.DecreaseAttacker);
        DealDamageAction.AddListener(heroSkill.DealDamage);
        DestroyAction.AddListener(heroSkill.Destroy);
    }
    public void IncreaseAttacker()
    {
        IncreaseAttackerAction?.Invoke();
    }
    public void DescreaseAttacker()
    {
        DecreaseAttackerAction?.Invoke();
    }
    public void DealDamage()
    {
        DealDamageAction?.Invoke();
    }
    public void Destroy()
    {
        DestroyAction?.Invoke();
    }
}
