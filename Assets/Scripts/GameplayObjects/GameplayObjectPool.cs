using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayObjectPool : IObjectPool
{
    #region Constants



    #endregion

    #region Objects Pools

    //TODO: change from gameobject to relevant interface
    private Queue<BaseRoad> _inActiveRoadsOP = new Queue<BaseRoad>();

    private Queue<BaseObstacle> _inActiveObstaclesOP = new Queue<BaseObstacle>();

    #endregion

    #region Factories

    private SimpleRoadFactory _simpleRoadFactory = new SimpleRoadFactory();
    private AsteroidFactory _asteroidFactory = new AsteroidFactory();

    #endregion

    #region Public Properties


    #endregion

    #region Methods

    public void AddObjectToPool(IPooledObject obj)
    {
        switch (obj.Type)
        {
            case PooledObjectType.Road:
                _inActiveRoadsOP.Enqueue(obj.GetGameObject().GetComponent<BaseRoad>());
                break;

            case PooledObjectType.Obstacle:
                _inActiveObstaclesOP.Enqueue(obj.GetGameObject().GetComponent<BaseObstacle>());
                break;

            default:
                Debug.LogError("Unkown gameplay object type sent to object pool");
                break;
        }

        obj.GetGameObject().SetActive(false);
    }

    public GameObject GetObjectFromPool(PooledObjectType type)
    {
        switch (type)
        {
            case PooledObjectType.Road:
                if (_inActiveRoadsOP.Count > 0)
                {
                    var road = _inActiveRoadsOP.Dequeue();
                    road.gameObject.SetActive(true);
                    return road.gameObject;
                } 
                else
                {
                    var sinmpleRoad = (SimpleRoad)_simpleRoadFactory.Create();
                    return sinmpleRoad.GetGameObject();
                }
                    

            case PooledObjectType.Obstacle:
                if (_inActiveObstaclesOP.Count > 0)
                {
                    var obstacle = _inActiveObstaclesOP.Dequeue();
                    obstacle.gameObject.SetActive(true);
                    return obstacle.gameObject;
                } 
                else
                {
                    var asteroid = (AsteroidObstacle)_asteroidFactory.Create();
                    return asteroid.GetGameObject();
                }

            default:
                Debug.LogError("Unkown gameplay object type requested from object pool");
                return null;
        }
    }

    #endregion
}