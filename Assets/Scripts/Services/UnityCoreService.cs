using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityCoreService : MonoBehaviour, IUnityCoreService
{
    private readonly List<IUpdate> _updatables = new List<IUpdate>();
    private readonly List<IStart> _startables = new List<IStart>();

    public void RegisterToUpdate(IUpdate method)
    {
        _updatables.Add(method);
    }

    public void RegisterToStart(IStart method)
    {
        _startables.Add(method);
    }

    private void Start()
    {
        for (int i = 0; i < _startables.Count; i++)
        {
            _startables[i].Start();
        }
    }

    void Update()
    {
        for (int i = 0; i < _updatables.Count; i++)
        {
            _updatables[i].Update();
        }
    }
}
