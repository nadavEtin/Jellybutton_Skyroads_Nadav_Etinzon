using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameObjectPool
{
    void AddObjectToPool(GameplayObjectType type, GameObject obj);

    GameObject GetObjectFromPool(GameplayObjectType type);
}

public enum GameplayObjectType
{
    Road,
    Obstacle
}