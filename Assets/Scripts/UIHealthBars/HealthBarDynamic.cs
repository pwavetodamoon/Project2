using System;
using System.Collections;
using CombatSystem.Entity;
using CombatSystem.Entity.Utilities;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBarDynamic : HealthBarBase
{
    [SerializeField] private EntityCharacter target;
    public Vector2 offset = Vector2.zero;
    public EntityAction entityAction;
    public void SetTarget(EntityCharacter target)
    {
        FadeColorBack();
        this.target = target;
        entityAction = target.GetRef<EntityAction>();
        entityAction.OnDie += Destroy;
        entityAction.OnHealthChange += SetHealthBar;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (target != null)
        {
            entityAction.OnDie -= Destroy;
            entityAction.OnHealthChange -= SetHealthBar;
        }
    }
    private void Update()
    {
        if (target != null)
        {
            var position = target.transform.position;
            transform.position = new Vector2(position.x + offset.x, position.y + offset.y);
        }
    }
    public void Destroy()
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
