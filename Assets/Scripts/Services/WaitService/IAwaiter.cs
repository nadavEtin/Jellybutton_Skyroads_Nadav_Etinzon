using System;

public interface IAwaiter
{
    IAwaiter OnStart(Action startCallback);

    IAwaiter OnProgress(Action progressCallback);

    IAwaiter OnEnd(Action endCallback);
}
