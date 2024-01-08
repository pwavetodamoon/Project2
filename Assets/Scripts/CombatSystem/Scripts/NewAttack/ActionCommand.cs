using System.Collections;
using UnityEngine;
using System;
using DG.Tweening;
public class ActionCommand : ICommand
{
    public bool isDone { get; set; }
    public float time;
    public Action action;
    public IEnumerator Execute()
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
        Debug.Log("Done action");
        isDone = true;
    }
}
public class GoToCommand : ICommand
{
    public bool isDone { get; set; }
    public float time;
    public Vector2 target;
    public Transform transform;
    public IEnumerator Execute()
    {
        //yield return new WaitForSeconds(time);
        yield return transform.DOMove(target, time).WaitForCompletion();
        Debug.Log("Done action");
        isDone = true;
    }
}
