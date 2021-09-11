using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayElements : MonoSingleton<GameplayElements>
{
    #region Editor

    [SerializeField] private PlayerShip _playerShip;
    [SerializeField] private RoadController _roadController;
    [SerializeField] private ObstacleController _obstacleController;
    [SerializeField] private int _roadLength;

    #endregion

    #region Fields

    private GameplayObjectPool _gop;

    #endregion

    #region Properties

    public IPlayer PlayerShip => _playerShip;
    public IObjectPool GameplayObjectPool => _gop;
    [HideInInspector]
    public Bounds RoadPieceSize;
    public int RoadLength => _roadLength;

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();
        _gop = new GameplayObjectPool();
    }

    public void CreateLevelRoad()
    {
        _roadController.CreateLevelRoad();
    }

    public void CreateObstacles()
    {
        _obstacleController.CreateLevelObstacles();
    }

    public void AddObstacleToRoad(GameObject obstacle, int roadPiece, Vector3 pos)
    {
        _roadController.AddObstacleToRoad(obstacle, roadPiece, pos);
    }

    public void RoadPulled(bool hadObstacle)
    {
        _obstacleController.RoadPulled(hadObstacle);
    }

    protected override GameplayElements ProvideInstance()
    {
        return this;
    }

    #endregion
}
