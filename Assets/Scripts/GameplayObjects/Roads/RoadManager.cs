using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    #region Editor

    [SerializeField] private RoadMovementController _roadMoveController;

    #endregion 

    #region Variables

    private int _curRoadStep;
    private List<BaseRoad> _activeRoadObjects = new List<BaseRoad>();
    private bool _shouldPerformPullCheck;
    public float _roadPieceLength { private set; get; }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        EventBus.Instance.Subscribe(GameplayEventType.StartGame, StartGameRM);
    }

    private void FixedUpdate()
    {
        if(_shouldPerformPullCheck)
            RoadPullCheck();
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(GameplayEventType.StartGame, StartGameRM);
    }

    #endregion

    #region Methods

    private void RoadPullCheck()
    {
        //check if the first road piece moved out of the game view 
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
        //disable the piece and move it to appropriate object pool
        GameplayElements.Instance.GameplayObjectPool.AddObjectToPool(_activeRoadObjects[0].ObjectType, _activeRoadObjects[0].gameObject);

        //notify obstacle manager a road was pulled and if it had an obstacle
        GameplayElements.Instance.RoadPulled(_activeRoadObjects[0].HasObstacle);
        if (_activeRoadObjects[0].HasObstacle)
            _activeRoadObjects[0].ObstacleRemoved();
        _activeRoadObjects.RemoveAt(0);
    }

    private GameObject AddNewRoadPiece()
    {
        //add new piece to the end of the road
        GameObject newRoad = GameplayElements.Instance.GameplayObjectPool.GetObjectFromPool(GameplayObjectType.Road);
        _activeRoadObjects.Add(newRoad.GetComponent<BaseRoad>());
        if (_roadPieceLength == 0)
        {
            //if this is the first road piece created init some variables
            _roadPieceLength = newRoad.transform.localScale.z;
            GameplayElements.Instance.RoadPieceSize = newRoad.GetComponent<BoxCollider>().bounds;
        }  

        return newRoad;
    }

    private void PositionRoadPiece(GameObject road, Vector3 pos = default)
    {
        if (pos == default)
        {
            pos = new Vector3(0, 0, _activeRoadObjects.Count * _roadPieceLength);
            //move the first piece slightly back to prevent pull errors
            if (_activeRoadObjects.Count == 1)
                pos.z -= 0.1f;
        }

        road.transform.localPosition = pos;
    }

    public void CreateLevelRoad()
    {
        //initial creation of the level's road
        for (int i = 0; i < GameplayElements.Instance.RoadLength; i++)
        {
            var road = AddNewRoadPiece();
            PositionRoadPiece(road);
            road.transform.SetParent(transform);
        }

        _roadMoveController.InitVariables(ref _activeRoadObjects);
    }

    public void AddObstacleToRoad(GameObject obstacle, int index, Vector3 obsPos)
    {
        _activeRoadObjects[index].AddObstacle(obstacle);
        obstacle.transform.localPosition = obsPos;
    }

    private void StartGameRM(BaseEventParams par)
    {
        _shouldPerformPullCheck = true;
    }

    #endregion
}