using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfrastructureServices
{
    #region Services

    private static UnityCoreService _ucs;
    //private static GameplayObjectPool _gop;

    #endregion

    #region Methods

    public static void Initialize()
    {
        //_gop = new GameplayObjectPool();
        _ucs = CreateUnityCore();
        _ucs.RegisterToUpdate(new InputManager());
    }

    private static UnityCoreService CreateUnityCore()
    {
        var go = new GameObject("UnityCoreService");
        Object.DontDestroyOnLoad(go);
        return go.AddComponent<UnityCoreService>();
    }

    #endregion
}
