using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAction : MonoBehaviour
{
    public Action OnRebirth;
    public Action OnDie;
    public Action OnTakeDamage;
    public Action<float> OnHealthChange;

    private void OnDisable()
    {
        OnRebirth = null;
        OnDie = null;
        OnTakeDamage = null;
        OnHealthChange = null;
    }
}
