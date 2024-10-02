using System.Collections;
using UnityEngine;

namespace Code.Infrastructure.Runners
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}