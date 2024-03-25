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
        Debug.Log("Invoking Unity Action");
        unityAction?.Invoke();
    }
    public void Invoke2()
    {
        Debug.Log("Invoking Unity Action 2");
        unityAction2?.Invoke();
    }
}
