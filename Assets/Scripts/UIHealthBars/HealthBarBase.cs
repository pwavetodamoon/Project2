using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using SlotHero;
using UnityEngine;
using UnityEngine.UI;
public abstract class HealthBarBase : MonoBehaviour
{
    public Image fill;
    public Image border;
    private void Awake()
    {
        SetFade(0, 0f);
    }

    protected virtual void OnDisable()
    {
        transform.DOKill();
    }

    [Button]
    public void FadeColor()
    {
        SetFade(0, .5f);
    }
    [Button]
    public void FadeColorBack()
    {
        SetFade(1);
    }
    protected void SetFade(int value, float time = 1)
    {
        fill.DOFade(value, time);
        border.DOFade(value, time);
    }
    private Vector2 FormatToWolrdPoint(Vector2 position)
    {
        var screenPoint = Camera.main.WorldToScreenPoint(position);
        return (Vector2)screenPoint;
    }
    [Button]
    public virtual void SetHealthBar(float value)
    {
        if (value < 0)
        {
            value = 0;
        }
        if (value > 1)
        {
            value = 1;
        }
        // transform.DOKill();
        transform.DOPunchScale(Vector3.one * .1f, .2f);
        fill.fillAmount = value;
        // fill.DOFillAmount(value, .2f);
    }
}
