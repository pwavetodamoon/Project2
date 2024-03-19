using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using SlotHero;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image fill;
    public Image border;
    public float xPos = -.8f;
    public float yPos = .3f;
    private void Awake()
    {
        SetFade(0, 0f);
    }
    public void SetPosition(Vector2 position)
    {
        position = new Vector3(position.x + xPos, position.y + yPos);
        var slotPosition = Camera.main.WorldToScreenPoint(position);
        transform.position = new Vector3(slotPosition.x, slotPosition.y);
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
    private void SetFade(int value, float time = 1)
    {
        fill.DOFade(value, time);
        border.DOFade(value, time);
    }

    [Button]
    public void SetHealthBar(float value)
    {
        if (value < 0)
        {
            value = 0;
        }
        if (value > 1)
        {
            value = 1;
        }
        fill.fillAmount = value;
    }
}
