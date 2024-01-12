using System.Collections;
using UnityEngine;
using DG.Tweening;

public struct GoToCommand : ICommandBehavior
{
    public Transform Transform;
    public Vector2 Target;
    public float Time;
    public IEnumerator FirstBehaviour()
    {
        yield return Transform.DOMove(Target, Time).WaitForCompletion();
    }
}
