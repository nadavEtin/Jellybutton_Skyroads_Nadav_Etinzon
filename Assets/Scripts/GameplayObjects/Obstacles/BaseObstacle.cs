using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    #region Private Fields

    protected GameplayObjectType _type;

    #endregion

    #region Public Properties

    public GameplayObjectType ObjectType => _type;

    #endregion

    #region Unity Methods

    protected virtual void Awake()
    {
        _type = GameplayObjectType.Obstacle;
    }

    #endregion
}