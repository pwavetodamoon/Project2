using System.Collections;
using UnityEngine;

namespace CombatSystem.Attack.Utilities
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}