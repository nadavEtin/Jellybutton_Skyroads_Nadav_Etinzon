using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfrastructureServices
{
    #region Services

    private static UnityCoreService _unityCoreService;
    private static AwaitService _awaitService;
    private static CoroutineService _coroutineService;

    #endregion

    #region Managers

    private static InputManager _inputMan;

    #endregion

    #region Properties

    public static IUnityCoreService UnityCoreService => _unityCoreService;
    public static IWaitService AwaitService => _awaitService;
    public static ICoroutineService CoroutineService => _coroutineService;

    #endregion

    #region Methods

    public static void Initialize()
    {
        _awaitService = CreateAwaitService();
        _coroutineService = CreateCoroutineService();
        _unityCoreService = CreateUnityCore();
        _inputMan = new InputManager();
        _unityCoreService.RegisterToUpdate(_inputMan);

    }

    private static UnityCoreService CreateUnityCore()
    {
        var go = new GameObject("UnityCoreService");
        return go.AddComponent<UnityCoreService>();
    }

    private static AwaitService CreateAwaitService()
    {
        var _as = new GameObject("AwaitService");
        return _as.AddComponent<AwaitService>();
    }

    private static CoroutineService CreateCoroutineService()
    {
        var _crs = new GameObject("CoroutineService");
        return _crs.AddComponent<CoroutineService>();
    }

    #endregion
}
