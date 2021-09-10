using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoroutineService
{
    Coroutine RunCoroutine(IEnumerator coroutineBody);
}
