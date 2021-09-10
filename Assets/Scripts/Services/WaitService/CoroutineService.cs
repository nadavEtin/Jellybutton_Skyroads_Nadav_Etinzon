using System.Collections;
using UnityEngine;

public class CoroutineService : MonoBehaviour, ICoroutineService
{
    public Coroutine RunCoroutine(IEnumerator coroutineBody)
    {
        return StartCoroutine(coroutineBody);
    }
}
