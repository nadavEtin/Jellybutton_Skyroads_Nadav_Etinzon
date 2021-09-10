using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    #region Editor

    [SerializeField] private float _moveSpeed = 4;

    #endregion 

    #region Private Variables

    //TODO: seperate road movement to new class

    private bool _shouldMove;
    private bool _obstaclesAreAttached;
    private int _curRoadStep = 0, _lastRoadStep = 0;
    private float _roadPieceLength = 0;
    private List<BaseRoad> _activeRoadObjects = new List<BaseRoad>();
    private Vector3 _moveDir = Vector3.back;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        EventBus.Instance.Subscribe(GameplayEventType.StartGame, GameStart);
    }

    private void FixedUpdate()
    {
        if (_shouldMove)
            MoveRoad();
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(GameplayEventType.StartGame, GameStart);
    }

    #endregion

    #region Methods

    private void MoveRoad()
    {
        for (int i = 0; i < _activeRoadObjects.Count; i++)
        {
            _activeRoadObjects[i].transform.position += _moveDir * _moveSpeed * Time.fixedDeltaTime;
        }

        RoadPullCheck();
    }

    private void RoadPullCheck()
    {
        _curRoadStep = (int)(_activeRoadObjects[0].transform.position.z / _roadPieceLength);
        if (_curRoadStep != _lastRoadStep)
        {
            PullRoadPiece();
            var newRoad = AddNewRoadPiece();
            PositionRoadPiece(newRoad, new Vector3(0, 0, _activeRoadObjects[_activeRoadObjects.Count - 2].transform.position.z + _roadPieceLength));
        }
    }

    private void PullRoadPiece()
    {
        GameplayElements.Instance.GameplayObjectPool.AddObjectToPool(_activeRoadObjects[0].GetComponent<BaseRoad>());
        if (_activeRoadObjects[0].HasObstacle)
        {
            GameplayElements.Instance.RoadPulled(_activeRoadObjects[0].HasObstacle);
            _activeRoadObjects[0].ObstacleRemoved();
        }
            
        _activeRoadObjects.RemoveAt(0);
        
    }

    private GameObject AddNewRoadPiece()
    {
        GameObject newRoad = GameplayElements.Instance.GameplayObjectPool.GetObjectFromPool(PooledObjectType.Road);
        _activeRoadObjects.Add(newRoad.GetComponent<BaseRoad>());
        if (_roadPieceLength == 0)
        {
            _roadPieceLength = newRoad.transform.localScale.z;
            GameplayElements.Instance.RoadPieceSize = newRoad.GetComponent<BoxCollider>().bounds;
        }  

        return newRoad;
    }

    private void PositionRoadPiece(GameObject road, Vector3 pos = default)
    {
        if (pos == default)
            road.transform.localPosition = new Vector3(0, 0, _activeRoadObjects.Count * _roadPieceLength);
        else
            road.transform.localPosition = pos;
    }

    public void CreateLevelRoad()
    {
        for (int i = 0; i < GameplayElements.Instance.RoadLength; i++)
        {
            var road = AddNewRoadPiece();
            PositionRoadPiece(road);
            road.transform.SetParent(transform);
        }  
    }

    public void AddObstacleToRoad(GameObject obstacle, int index, Vector3 obsPos)
    {
        //_obstaclesAreAttached = true;
        //obstacle.transform.SetParent(_activeRoadObjects[index].transform);
        _activeRoadObjects[index].AddObstacle(obstacle);
        obstacle.transform.localPosition = obsPos;
    }

    private void GameStart(BaseEventParams par)
    {
        _shouldMove = true;
    }

    #endregion
}