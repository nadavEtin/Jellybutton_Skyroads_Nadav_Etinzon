using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayElements : MonoSingleton<GameplayElements>
{
    #region Editor

    [SerializeField] private PlayerShip _playerShip;
    [SerializeField] private RoadManager _roadController;
    [SerializeField] private ObstacleManager _obstacleController;
    [SerializeField] private int _roadLength;

    #endregion

    #region Private Fields

    private GameplayObjectPool _gop;

    #endregion

    #region Public Properties

    public IPlayer PlayerShip => _playerShip;
    public IGameObjectPool GameplayObjectPool => _gop;
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

    //mediates between gameLoop and specific manager
    public void CreateLevelRoad()
    {
        _roadController.CreateLevelRoad();
    }

    //mediates between gameLoop and specific manager
    public void CreateObstacles()
    {
        _obstacleController.CreateLevelObstacles();
    }

    public void AddObstacleToRoad(GameObject obstacle, int roadPiece, Vector3 pos)
    {
        _roadController.AddObstacleToRoad(obstacle, roadPiece, pos);
    }

    //mediates between road and obstacle managers
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