using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactory : ObstacleBaseFactory
{
    #region Constants

    private const string OBSTACLES_RESOURCE_NAME = "ScriptableObjects/Obstacle Params";

    #endregion

    #region Fields

    private ObstacleParams _obstacleParams;

    #endregion

    #region Methods

    public AsteroidFactory()
    {
        _obstacleParams = Resources.Load<ObstacleParams>(OBSTACLES_RESOURCE_NAME);
    }

    public override IObstacle Create()
    {
        var asteroid = (GameObject)Object.Instantiate(_obstacleParams.AsteroidPrefab);
        return asteroid.GetComponent<AsteroidObstacle>();
    }

    #endregion
}
