using System.Collections;
using UnityEngine;

namespace NewCombat.Attack.Utilities
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}