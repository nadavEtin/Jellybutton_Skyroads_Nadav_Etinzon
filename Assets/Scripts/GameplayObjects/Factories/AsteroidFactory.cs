using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactory : GameObjectBaseFactory
{
    #region Constants

    private const string OBSTACLES_RESOURCE_NAME = "ScriptableObjects/ObstacleParams";

    #endregion

    #region Private Fields

    private ObstacleParams _obstacleParams;

    #endregion

    #region Methods

    public AsteroidFactory()
    {
        _obstacleParams = Resources.Load<ObstacleParams>(OBSTACLES_RESOURCE_NAME);
    }

    public override GameObject Create()
    {
        var asteroid = (GameObject)Object.Instantiate(_obstacleParams.AsteroidPrefab);
        return asteroid;
    }

    #endregion
}