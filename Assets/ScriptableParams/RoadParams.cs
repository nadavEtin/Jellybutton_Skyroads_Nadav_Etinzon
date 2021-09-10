using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/Road Params", fileName = "Road Params")]
public class RoadParams : ScriptableObject
{
    [SerializeField] private Object _roadPrefab;
    [SerializeField] private Object _multiRoadPrefab;

    public Object SingleRoadPrefab => _roadPrefab;
    public Object MultiRoadPrefab => _multiRoadPrefab;
}
