using System.Collections;
using UnityEngine;

namespace NewCombat
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}