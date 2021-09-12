using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRoad : MonoBehaviour, IRoad
{
    #region Private Fields

    protected GameplayObjectType _type;
    protected bool _hasObstacle;

    #endregion

    #region Public Properties

    public GameplayObjectType ObjectType => _type;
    public bool HasObstacle => _hasObstacle;

    #endregion

    #region Methods

    protected virtual void Awake()
    {
        _type = GameplayObjectType.Road;
    }

    //adds an obstacle as a chile of this road
    public virtual void AddObstacle(GameObject obstacle)
    {
        obstacle.transform.SetParent(transform);
        _hasObstacle = true;
    }

    public virtual void ObstacleRemoved()
    {
        _hasObstacle = false;
    }

    #endregion
}
