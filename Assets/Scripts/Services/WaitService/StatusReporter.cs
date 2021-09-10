using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusReporter : IAwaiter
{
    private Action _onProgress, _onStart, _onEnd;

    public IAwaiter OnStart(Action startCallback)
    {
        _onStart = startCallback;
        return this;
    }

    public IAwaiter OnProgress(Action progressCallback)
    {
        _onProgress = progressCallback;
        return this;
    }

    public IAwaiter OnEnd(Action endCallback)
    {
        _onEnd = endCallback;
        return this;
    }

    public void ReportProgress()
    {
        _onProgress?.Invoke();
    }

    public void ReportStart()
    {
        _onStart?.Invoke();
    }

    public void ReportDone()
    {
        _onEnd?.Invoke();
    }
}
