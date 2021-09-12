using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoadFactory : GameObjectBaseFactory
{
    #region Constants

    private const string ROADS_RESOURCE_NAME = "ScriptableObjects/RoadParams";

    #endregion

    #region Private Fields

    private RoadParams _roadPrefabRef;

    #endregion

    #region Methods

    public SimpleRoadFactory()
    {
        _roadPrefabRef = Resources.Load<RoadParams>(ROADS_RESOURCE_NAME);
    }

    public override GameObject Create()
    {
        var road = (GameObject)Object.Instantiate(_roadPrefabRef.SingleRoadPrefab);
        return road;
    }

    #endregion
}