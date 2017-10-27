using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineHelper
{
    public static IEnumerator ExecuteAfterTime(float time, System.Action action)
    {
        yield return new WaitForSeconds(time);

        if (action != null)
            action();
    }

    public static IEnumerator ExecuteAfterFrame(System.Action action)
    {
        yield return new WaitForFixedUpdate();

        if (action != null)
            action();
    }
}
