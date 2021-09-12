using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayObjectPool : IGameObjectPool
{
    #region Objects Pools

    private Queue<BaseRoad> _inActiveRoadsOP = new Queue<BaseRoad>();
    private Queue<BaseObstacle> _inActiveObstaclesOP = new Queue<BaseObstacle>();

    #endregion

    #region Factories

    private SimpleRoadFactory _simpleRoadFactory = new SimpleRoadFactory();
    private AsteroidFactory _asteroidFactory = new AsteroidFactory();

    #endregion

    #region Methods

    //store an inactive gameObject in the appropriate object pool
    public void AddObjectToPool(GameplayObjectType type, GameObject obj)
    {
        switch (type)
        {
            case GameplayObjectType.Road:
                _inActiveRoadsOP.Enqueue(obj.GetComponent<BaseRoad>());
                break;

            case GameplayObjectType.Obstacle:
                _inActiveObstaclesOP.Enqueue(obj.GetComponent<BaseObstacle>());
                break;

            default:
                Debug.LogError("Unkown gameplay object type sent to object pool");
                break;
        }

        obj.SetActive(false);
    }

    //return an inactive object if one exists, create new one if not
    public GameObject GetObjectFromPool(GameplayObjectType type)
    {
        switch (type)
        {
            case GameplayObjectType.Road:
                if (_inActiveRoadsOP.Count > 0)
                {
                    var road = _inActiveRoadsOP.Dequeue();
                    road.gameObject.SetActive(true);
                    return road.gameObject;
                } 
                else
                {
                    var simpleRoad = _simpleRoadFactory.Create();
                    return simpleRoad;
                }
                    

            case GameplayObjectType.Obstacle:
                if (_inActiveObstaclesOP.Count > 0)
                {
                    var obstacle = _inActiveObstaclesOP.Dequeue();
                    obstacle.gameObject.SetActive(true);
                    return obstacle.gameObject;
                } 
                else
                {
                    var asteroid = _asteroidFactory.Create();
                    return asteroid;
                }

            default:
                Debug.LogError("Unkown gameplay object type requested from object pool");
                return null;
        }
    }

    #endregion
}