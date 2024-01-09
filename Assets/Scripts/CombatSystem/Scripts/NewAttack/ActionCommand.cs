using System.Collections;
using UnityEngine;
using System;
using DG.Tweening;
public struct ActionCommand : ICommand
{
    public bool IsDone { get; set; }
    public float Time { get; set; }

    public Action CallbackMethod;
    public IEnumerator Execute()
    {
        yield return Behaviour();
        yield return new WaitForSeconds(Time);
        CallbackMethod?.Invoke();
        IsDone = true;
        Debug.Log("Done CallbackMethod");
    }
    public IEnumerator Behaviour()
    {
        yield return null;
    }
}
public class TestCM : ICommand
{
    public bool IsDone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IEnumerator Execute()
    {
        throw new NotImplementedException();
    }
}

public struct GoToCommand : ICommand
{
    public bool IsDone { get; set; }
    public float Time { get; set; }
    public Vector2 Target;
    public Transform Transform;

    public IEnumerator Execute()
    {
        yield return Behaviour();
        Debug.Log("Done CallbackMethod");
        IsDone = true;
    }
    IEnumerator Behaviour()
    {
        yield return Transform.DOMove(Target, Time).WaitForCompletion();
    }
}
public interface ICommand
{
    bool IsDone { get; set; }
    float Time { get; set; }
    IEnumerator Execute();
}
