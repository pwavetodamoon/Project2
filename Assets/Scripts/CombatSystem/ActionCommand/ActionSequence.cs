using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ActionSequence : MonoBehaviour
{
    List<ICommand> commands = new List<ICommand>();
    public bool isExcuted = false;
    public void AddCommand(ICommand command)
    {
        commands.Add(command);
    }
    private void Update()
    {
        if (commands.Count == 0 || isExcuted) return;
        //Debug.Log("Start sequence");
        StartCoroutine(StartExcute());
    }

    //[Button]
    IEnumerator StartExcute()
    {
        isExcuted = true;
        var command = commands[0];
        if (command.IsDone) yield break;

        yield return command.Execute();
        //Debug.Log("Done sequence");
        Remove(command);

        yield return new WaitForEndOfFrame();
        isExcuted = false;
    }
    public void Remove(ICommand command)
    {
        commands.Remove(command);
    }
}


