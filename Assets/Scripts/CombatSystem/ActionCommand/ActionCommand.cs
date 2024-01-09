using System.Collections;
using UnityEngine;
using System;
public struct ActionCommand : ICommand
{
    // Create a contructer to set the value of the command
    public ActionCommand(ICommandBehavior behaviour = null, Action callback = null, float time = 0)
    {
        CommandBehavior = behaviour;
        CallbackMethod = callback;
        Time = time;
        IsDone = false;
    }
    public bool IsDone { get; set; }
    public float Time { get; set; }

    public ICommandBehavior CommandBehavior;

    public Action CallbackMethod;
    public IEnumerator Execute()
    {
        yield return CommandBehavior?.Behaviour();
        yield return new WaitForSeconds(Time);
        CallbackMethod?.Invoke();
        IsDone = true;
        Debug.Log("Done CallbackMethod");
    }
    public void SetCallback(Action callback)
    {
        CallbackMethod = callback;
    }
    public void SetBehaviour(ICommandBehavior behaviour)
    {
        CommandBehavior = behaviour;
    }
}
