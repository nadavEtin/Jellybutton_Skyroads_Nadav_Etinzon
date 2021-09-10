using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Awake()
    {
        InfrastructureServices.Initialize();
    }
}
