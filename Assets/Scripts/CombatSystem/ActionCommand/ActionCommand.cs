using System.Collections;
using UnityEngine;
using System;
public struct ActionCommand : ICommand
{
    // Create a contructer to set the value of the command
    public ActionCommand(ICommandBehavior behaviour = null, Action callback = null, float time = 0)
    {
        CommandBehavior = behaviour;
        EndCallbackMethod = callback;
        Time = time;
        IsDone = false;
    }
    public bool IsDone { get; set; }
    public float Time { get; set; }

    public ICommandBehavior CommandBehavior;

    public Action EndCallbackMethod;
    public IEnumerator Execute()
    {
        yield return CommandBehavior?.FirstBehaviour();
        EndCallbackMethod?.Invoke();
        yield return new WaitForSeconds(Time);
        IsDone = true;
        Debug.Log("Done EndCallbackMethod");
    }
    public void SetCallback(Action callback)
    {
        EndCallbackMethod = callback;
    }
    public void SetBehaviour(ICommandBehavior behaviour)
    {
        CommandBehavior = behaviour;
    }
}
