using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ActionSequence : MonoBehaviour
{
    [ShowInInspector] List<ICommand> commands = new List<ICommand>();
    public bool isExcuted = false;
    public void AddCommand(ICommand command)
    {
        commands.Add(command);
    }
    private void Update()
    {
        if (commands.Count == 0 || isExcuted) return;
        Debug.Log("Start sequence");
        StartCoroutine(StartExcute());
    }
    //[Button]
    IEnumerator StartExcute()
    {
        isExcuted = true;
        if (commands[0].IsDone) yield break;

        yield return commands[0].Execute();
        Debug.Log("Done sequence");
        Remove(commands[0]);

        yield return new WaitForEndOfFrame();
        isExcuted = false;
    }
    void Test2(IEnumerator coroutine)
    {
        if (commands.Count == 0 || isExcuted || !commands[0].IsDone) return;
        StartCoroutine(coroutine);
    }
    public void Remove(ICommand command)
    {
        commands.Remove(command);
    }
}


