using System.Collections;
using UnityEngine;

public class AwaitService : MonoBehaviour, IWaitService
{
    public IAwaiter WaitFor(int waitDelay)
    {
        //create awaiter to track process start, progress and end
        //send awaiter refrence back to caller to track status
        var sr = new StatusReporter();
        InfrastructureServices.CoroutineService.RunCoroutine(WaitForInternal(waitDelay, sr));
        return sr;
    }

    private IEnumerator WaitForInternal(int waitDelay, StatusReporter sr)
    {
        yield return null;
        sr.ReportStart();
        for (int i = 0; i < waitDelay; i++)
        {
            yield return new WaitForSeconds(1);
            sr.ReportProgress();
        }
        sr.ReportDone();
    }
}
