using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    //entry point of the project
    private void Awake()
    {
        InfrastructureServices.Initialize();
        GameLoopManager.Instance.Initialize();
    }
}
