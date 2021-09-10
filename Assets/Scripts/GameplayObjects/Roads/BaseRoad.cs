using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRoad : MonoBehaviour, IPooledObject, IRoad
{
    #region Fields

    protected PooledObjectType _type;

    #endregion

    #region Properties

    public PooledObjectType Type => _type;

    #endregion

    #region Methods

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    #endregion
}
