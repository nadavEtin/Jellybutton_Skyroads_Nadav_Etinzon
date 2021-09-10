using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPool
{
    void AddObjectToPool(IPooledObject obj);

    GameObject GetObjectFromPool(PooledObjectType type);
}
