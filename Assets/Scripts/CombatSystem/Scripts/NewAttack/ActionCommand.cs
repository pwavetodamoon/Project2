using System.Collections;
using UnityEngine;
using System;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
public class ActionCommand : ICommand
{
    public bool IsDone { get; set; }
    public float Time { get; set; }

    public Action CallbackMethod;
    public virtual IEnumerator Execute()
    {
        yield return Behaviour();
        yield return new WaitForSeconds(Time);
        CallbackMethod?.Invoke();
        IsDone = true;
        Debug.Log("Done CallbackMethod");
    }
    protected virtual IEnumerator Behaviour()
    {
        yield return null;
    }
}
public class TestCommand : ActionCommand
{
    public Vector2 Target;
    public Transform Transform;

    protected override IEnumerator Behaviour()
    {
        yield return Transform.DOMove(Target, Time).WaitForCompletion();
    }
}
struct CommanConfig : ICommand
{
    public bool IsDone { get; set; }
    public float Time { get; set; }
    public IEnumerator Execute()
    {
        yield return new WaitForSeconds(Time);
        IsDone = true;
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
public interface ICommandExcute
{

}
