using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    #region Editor

    [Range(0, 100)]
    [SerializeField] private int _obstacleProbability = 15;
    [SerializeField] private int _spawnRateDifficultyIncrease = 4;
    [SerializeField] private float _obstacleHeight = 3f;
    [SerializeField] private float _obstacleSpawnZone = 0.35f;

    #endregion

    #region Private Variables

    private readonly List<BaseObstacle> _activeObstacleObjects = new List<BaseObstacle>();
    private float _spawnZoneMinX, _spawnZoneMaxX, _spawnZoneMinZ, _spawnZoneMaxZ;
    private int _lastRoadIndex;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        EventBus.Instance.Subscribe(GameplayEventType.IncreaseDifficulty, DifficultyIncreaseOM);
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(GameplayEventType.IncreaseDifficulty, DifficultyIncreaseOM);
    }

    #endregion

    #region Methods

    //randomly add obstacles on level creation
    public void CreateLevelObstacles()
    {
        InitializeVariables();

        //start from the 3rd road piece to prevent unwinnable levels
        for (int i = 2; i < GameplayElements.Instance.RoadLength; i++)
        {
            var newObs = RandomizeNewObstacle();
            if(newObs != null)
            {
                var pos = RandomPositionOnRoad();
                GameplayElements.Instance.AddObstacleToRoad(newObs, i, pos);
            }
        }
    }

    //remove obstacle from game and store it in its object pool
    public void PullObstacle()
    {
        GameplayElements.Instance.GameplayObjectPool.AddObjectToPool(_activeObstacleObjects[0].GetComponent<BaseObstacle>().ObjectType, _activeObstacleObjects[0].gameObject);
        _activeObstacleObjects.RemoveAt(0);

        //reward points for passing an obstacle
        GameLoopManager.Instance.ScoreManager.UpdateObstaclesPassed(1);
    }

    //called after a road was pulled
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
        _spawnZoneMinX = GameplayElements.Instance.RoadPieceSize.min.x;
        _spawnZoneMaxX = GameplayElements.Instance.RoadPieceSize.max.x;
        _spawnZoneMinZ = GameplayElements.Instance.RoadPieceSize.min.z;
        _spawnZoneMaxZ = GameplayElements.Instance.RoadPieceSize.max.z;
        _lastRoadIndex = GameplayElements.Instance.RoadLength - 1;
    }

    private GameObject RandomizeNewObstacle()
    {
        //randomly add new obstacle to level according to the current probability, determined by game difficulty
        var rnd = Random.Range(0, 100);
        if (rnd <= _obstacleProbability)
            return AddNewObstacle();
        else
            return null;
    }

    //position the obstacle randomly inside predefined zone on the road piece
    private Vector3 RandomPositionOnRoad()
    {
        var xPos = Random.Range(_spawnZoneMinX * _obstacleSpawnZone, _spawnZoneMaxX * _obstacleSpawnZone);
        var zPos = Random.Range(_spawnZoneMinZ * _obstacleSpawnZone, _spawnZoneMaxZ * _obstacleSpawnZone);
        return new Vector3(xPos, _obstacleHeight, zPos);
    }

    private GameObject AddNewObstacle()
    {
        var newObs = GameplayElements.Instance.GameplayObjectPool.GetObjectFromPool(GameplayObjectType.Obstacle);
        _activeObstacleObjects.Add(newObs.GetComponent<BaseObstacle>());
        return newObs;
    }

    private void DifficultyIncreaseOM(BaseEventParams par)
    {
        _obstacleProbability += _spawnRateDifficultyIncrease;
    }

    #endregion
}