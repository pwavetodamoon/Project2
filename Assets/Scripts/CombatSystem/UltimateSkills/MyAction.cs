using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyAction : MonoBehaviour
{
    public UnityEvent unityAction;
    public UnityEvent unityAction2;
    public void Invoke()
    {
        unityAction?.Invoke();
    }
    public void Invoke2()
    {
        unityAction2?.Invoke();
    }
}
