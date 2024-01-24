using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CombatSystem.ActionCommand.Behavior
{
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
}
