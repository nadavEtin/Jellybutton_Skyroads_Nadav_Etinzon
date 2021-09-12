using System;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour
{
    #region Private Fields

    private static T _instance;

    #endregion

    #region Public Properties

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new InvalidOperationException($"Have no instance if {typeof(T)} yet");
            }

            return _instance;
        }
    }

    #endregion

    #region methods

    protected virtual void Awake()
    {
        _instance = ProvideInstance();
    }

    protected abstract T ProvideInstance();

    #endregion
}
