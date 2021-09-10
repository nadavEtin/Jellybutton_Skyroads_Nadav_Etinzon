using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoad : BaseRoad
{
    #region Methods

    private void Awake()
    {
        _type = PooledObjectType.Road;
    }

    #endregion
}
