using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoadFactory : RoadBaseFactory
{
    #region Constants

    private const string ROADS_RESOURCE_NAME = "ScriptableObjects/Road Params";

    #endregion

    #region Fields

    private RoadParams _roadPrefabRef;

    #endregion

    #region Methods

    public SimpleRoadFactory()
    {
        _roadPrefabRef = Resources.Load<RoadParams>(ROADS_RESOURCE_NAME);
    }

    public override IRoad Create()
    {
        var road = (GameObject)Object.Instantiate(_roadPrefabRef.SingleRoadPrefab);
        return road.GetComponent<SimpleRoad>();
    }

    #endregion
}