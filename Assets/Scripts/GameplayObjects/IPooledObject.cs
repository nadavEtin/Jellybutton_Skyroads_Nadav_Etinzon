using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PooledObjectType
{
    Road,
    Obstacle
}

public interface IPooledObject
{
    PooledObjectType Type { get; }

    GameObject GetGameObject();
}
