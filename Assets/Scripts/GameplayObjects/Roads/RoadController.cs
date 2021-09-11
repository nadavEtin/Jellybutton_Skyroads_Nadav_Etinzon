using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    #region Editor

    [SerializeField] private float _speedUpOnDiffIncrease = 1;
    [SerializeField] private float _moveSpeed = 4;
    [SerializeField] private float _boostSpeedMultiplier = 2;

    #endregion 

    #region Private Variables

    //TODO: seperate road movement to new class

    private bool _shouldMove;
    private int _curRoadStep;
    private float _roadPieceLength;
    private readonly List<BaseRoad> _activeRoadObjects = new List<BaseRoad>();
    private Vector3 _moveDir = Vector3.back;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        EventBus.Instance.Subscribe(GameplayEventType.StartGame, GameStart);
        EventBus.Instance.Subscribe(GameplayEventType.IncreaseDifficulty, DifficultyIncrease);
        EventBus.Instance.Subscribe(GameplayEventType.BoostClicked, BoostClicked);
        EventBus.Instance.Subscribe(GameplayEventType.GameOver, GameOver);
    }

    private void FixedUpdate()
    {
        if (_shouldMove)
            MoveRoad();
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(GameplayEventType.StartGame, GameStart);
        EventBus.Instance.Unsubscribe(GameplayEventType.IncreaseDifficulty, DifficultyIncrease);
        EventBus.Instance.Unsubscribe(GameplayEventType.BoostClicked, BoostClicked);
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
        if (_curRoadStep != 0)
        {
            PullRoadPiece();
            var newRoad = AddNewRoadPiece();
            PositionRoadPiece(newRoad, new Vector3(0, 0, _activeRoadObjects[_activeRoadObjects.Count - 2].transform.position.z + _roadPieceLength));
        }
    }

    private void PullRoadPiece()
    {
        GameplayElements.Instance.GameplayObjectPool.AddObjectToPool(_activeRoadObjects[0].GetComponent<BaseRoad>());
        GameplayElements.Instance.RoadPulled(_activeRoadObjects[0].HasObstacle);
        if (_activeRoadObjects[0].HasObstacle)
            _activeRoadObjects[0].ObstacleRemoved();
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

    private void GameOver(BaseEventParams par)
    {
        _shouldMove = false;
    }

    private void GameStart(BaseEventParams par)
    {
        _shouldMove = true;
    }

    private void DifficultyIncrease(BaseEventParams par)
    {
        _moveSpeed += _speedUpOnDiffIncrease;
    }

    private void BoostClicked(BaseEventParams par)
    {
        var boostParam = (BoostParams)par;
        if (boostParam.BoostIsOn)
        {
            //increase speed while boost is on
            _moveSpeed *= _boostSpeedMultiplier;
            _speedUpOnDiffIncrease *= _boostSpeedMultiplier;
        }
        else
        {
            //go back to normal speed
            _moveSpeed /= _boostSpeedMultiplier;
            _speedUpOnDiffIncrease /= _boostSpeedMultiplier;
        }
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
        _activeRoadObjects[index].AddObstacle(obstacle);
        obstacle.transform.localPosition = obsPos;
    }

    #endregion
}