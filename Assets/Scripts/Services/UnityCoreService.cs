using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityCoreService : MonoBehaviour, IUnityCoreService
{
    private readonly List<IUpdatable> _updatables = new List<IUpdatable>();

    public void RegisterToUpdate(IUpdatable method)
    {
        _updatables.Add(method);
    }

    void Update()
    {
        for (int i = 0; i < _updatables.Count; i++)
        {
            _updatables[i].Update();
        }
    }
}
