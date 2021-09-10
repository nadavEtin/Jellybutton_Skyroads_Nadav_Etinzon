using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRoad : MonoBehaviour, IPooledObject, IRoad
{
    #region Fields

    protected PooledObjectType _type;
    protected bool _hasObstacle;

    #endregion

    #region Properties

    public PooledObjectType Type => _type;
    public bool HasObstacle => _hasObstacle;

    #endregion

    #region Methods

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void AddObstacle(GameObject obstacle)
    {
        obstacle.transform.SetParent(transform);
        _hasObstacle = true;
    }

    public void ObstacleRemoved()
    {
        _hasObstacle = false;
    }

    #endregion
}
