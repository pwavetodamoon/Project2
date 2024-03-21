using System;
using System.Collections;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarDynamic : HealthBarBase
{
    [SerializeField] private EntityCharacter target;
    public Vector2 offset = Vector2.zero;
    public void SetTarget(EntityCharacter target)
    {
        FadeColorBack();
        this.target = target;
        target.GetComponent<EntityStateManager>().OnDie += Destroy;
        target.GetEntityStats().OnHealthChange += SetHealthBar;
    }

    private void Update()
    {
        if (target != null)
        {
            var position = target.transform.position;
            transform.position = new Vector2(position.x + offset.x, position.y + offset.y);
        }
    }
    public override void SetHealthBar(float value)
    {
        if (value < 0)
        {
            value = 0;
        }
        if (value > 1)
        {
            value = 1;
        }
        Debug.Log(value);
        fill.fillAmount = value;
    }
    private void Destroy()
    {
        StartCoroutine(DestroyAfter());
    }

    private IEnumerator DestroyAfter()
    {
        FadeColorBack();
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
