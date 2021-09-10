using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    #region Editor

    [Range(0, 100)]
    [SerializeField] private float _obstacleProbability = 15;
    [SerializeField] private float _obstacleHeight = 0.35f;

    #endregion

    #region Private Variables

    private List<BaseObstacle> _activeObstacleObjects = new List<BaseObstacle>();
    //private Vector3 _obstacleSpawnZone;
    private float _spawnZoneMinX, _spawnZoneMaxX, _spawnZoneMinZ, _spawnZoneMaxZ;
    private int _lastRoadIndex;

    #endregion

    #region Methods

    public void CreateLevelObstacles()
    {
        InitializeVariables();

        for (int i = 0; i < GameplayElements.Instance.RoadLength; i++)
        {
            var newObs = RandomizeNewObstacle();
            if(newObs != null)
            {
                var pos = RandomPositionOnRoad();
                GameplayElements.Instance.AddObstacleToRoad(newObs, i, pos);
            }
        }
    }

    public void PullObstacle()
    {
        GameplayElements.Instance.GameplayObjectPool.AddObjectToPool(_activeObstacleObjects[0].GetComponent<BaseObstacle>());
        _activeObstacleObjects.RemoveAt(0);
    }

    public void RoadPulled(bool hadObstacle)
    {
        if(hadObstacle)
            PullObstacle();
        var newObs = RandomizeNewObstacle();
        if (newObs != null)
        {
            var pos = RandomPositionOnRoad();
            GameplayElements.Instance.AddObstacleToRoad(newObs, _lastRoadIndex, pos);
        }
    }

    private void InitializeVariables()
    {
        //_obstacleSpawnZone = GameplayElements.Instance.RoadPieceSize;
        _spawnZoneMinX = GameplayElements.Instance.RoadPieceSize.min.x;
        _spawnZoneMaxX = GameplayElements.Instance.RoadPieceSize.max.x;
        _spawnZoneMinZ = GameplayElements.Instance.RoadPieceSize.min.z;
        _spawnZoneMaxZ = GameplayElements.Instance.RoadPieceSize.max.z;
        _lastRoadIndex = GameplayElements.Instance.RoadLength - 1;
    }

    private GameObject RandomizeNewObstacle()
    {
        var rnd = Random.Range(0, 100);
        if (rnd <= _obstacleProbability)
            return AddNewObstacle();
        else
            return null;
    }

    private Vector3 RandomPositionOnRoad()
    {
        var xPos = Random.Range(_spawnZoneMinX * 0.35f, _spawnZoneMaxX * 0.35f);
        var zPos = Random.Range(_spawnZoneMinZ * 0.35f, _spawnZoneMaxZ * 0.35f);
        return new Vector3(xPos, _obstacleHeight, zPos);
    }

    private GameObject AddNewObstacle()
    {
        var newObs = GameplayElements.Instance.GameplayObjectPool.GetObjectFromPool(PooledObjectType.Obstacle);
        _activeObstacleObjects.Add(newObs.GetComponent<BaseObstacle>());
        return newObs;
    }



    #endregion

    #region Unity Methods


    #endregion

    #region

    #endregion
}
