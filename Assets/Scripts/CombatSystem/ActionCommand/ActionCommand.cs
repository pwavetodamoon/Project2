using System.Collections;
using UnityEngine;
using System;
public struct ActionCommand : ICommand
{
    // Create a contructer to set the value of the command
    public ActionCommand(IEnumerator coroutine = null,Action callback = null, float time = 0)
    {
        EndCallbackMethod = callback;
        Time = time;
        IsDone = false;
        Coroutine = coroutine;
    }
    public bool IsDone { get; set; }
    public float Time { get; set; }
    private IEnumerator Coroutine;

    public Action EndCallbackMethod;
    public IEnumerator Execute()
    {
        yield return Coroutine;
        EndCallbackMethod?.Invoke();
        yield return new WaitForSeconds(Time);
        IsDone = true;
    }
    public void SetCallback(Action callback)
    {
        EndCallbackMethod = callback;
    }
    public void SetCoroutine(IEnumerator coroutine)
    {
        Coroutine = coroutine;
    }
}
