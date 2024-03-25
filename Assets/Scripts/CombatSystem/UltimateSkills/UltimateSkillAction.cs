using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UltimateSkillAction : MonoBehaviour
{
    public UnityEvent unityAction;
    public UnityEvent unityAction2;
    public HeroSkill heroSkill;
    private void Awake()
    {
        heroSkill = GetComponentInParent<HeroSkill>();
        unityAction.AddListener(heroSkill.DealDamage);
        unityAction2.AddListener(heroSkill.Destroy);
    }
    public void Invoke()
    {
        unityAction?.Invoke();
    }
    public void Invoke2()
    {
        unityAction2?.Invoke();
    }
}
