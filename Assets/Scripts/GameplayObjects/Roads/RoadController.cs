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
    private int _curRoadStep = 0, _lastRoadStep = 0;
    private float _roadPieceLength = 0;
    private List<BaseRoad> _activeRoadObj = new List<BaseRoad>();
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
        for (int i = 0; i < _activeRoadObj.Count; i++)
        {
            _activeRoadObj[i].transform.position += _moveDir * _moveSpeed * Time.fixedDeltaTime;
        }

        RoadPullCheck();
    }

    private void RoadPullCheck()
    {
        _curRoadStep = (int)(_activeRoadObj[0].transform.position.z / _roadPieceLength);
        if (_curRoadStep != _lastRoadStep)
        {
            PullRoadPiece();
            var road = AddNewRoadPiece();
            PositionRoadPiece(road, new Vector3(0, 0, _activeRoadObj[_activeRoadObj.Count - 2].transform.position.z + _roadPieceLength));
        }
    }

    private void PullRoadPiece()
    {
        GameplayElements.Instance.SendObjectToPool(_activeRoadObj[0].GetComponent<BaseRoad>());
        _activeRoadObj.RemoveAt(0);
    }

    private GameObject AddNewRoadPiece()
    {
        GameObject newRoad = GameplayElements.Instance.GetGameplayObject(PooledObjectType.Road);
        newRoad.SetActive(true);
        _activeRoadObj.Add(newRoad.GetComponent<BaseRoad>());
        if (_roadPieceLength == 0)
        {
            _roadPieceLength = newRoad.transform.localScale.z;
            GameplayElements.Instance.RoadPieceSize = newRoad.transform.localScale;
        }  

        return newRoad;
    }

    private void PositionRoadPiece(GameObject road, Vector3 pos = default)
    {
        if (pos == default)
            road.transform.localPosition = new Vector3(0, 0, _activeRoadObj.Count * _roadPieceLength);
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

    private void GameStart(EventParams par)
    {
        _shouldMove = true;
    }

    #endregion
}