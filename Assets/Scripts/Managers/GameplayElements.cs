﻿using System.Collections;
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

    //public GameObject RoadsContainer => _roadContainer;
    [HideInInspector]
    public Vector3 RoadPieceSize;

    //TODO: remove this and sent the number in the eventParams of the event
    public int RoadLength => _roadLength;

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();
        _gop = new GameplayObjectPool();
    }

    public GameObject GetGameplayObject(PooledObjectType type)
    {
        return _gop.GetObjectFromPool(type);
    }

    public void SendObjectToPool(IPooledObject obj)
    {
        _gop.AddObjectToPool(obj);
    }

    //TODO: move this to roadcontroller anf subscribe it to an event once the event bus exists
    public void CreateLevelRoad()
    {
        _roadController.CreateLevelRoad();
    }

    public void CreateObstacles()
    {
        _obstacleController.CreateLevelObstacles();
    }

    public void AddObstacleToRoad(GameObject obstacle, int roadPiece)
    {

    }

    protected override GameplayElements ProvideInstance()
    {
        return this;
    }

    #endregion
}